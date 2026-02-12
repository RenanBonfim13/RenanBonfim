using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.DatabaseServices;
using System;
using System.Collections.Generic;

namespace Civil3DPointLine
{
    /// <summary>
    /// Plugin para AutoCAD Civil 3D 2026 - Cria linhas a partir de pontos selecionados
    /// </summary>
    public class PointLineCommands
    {
        /// <summary>
        /// Comando para selecionar vários pontos do Civil 3D e criar uma linha
        /// Uso: Digite POINTLINE no prompt de comando do Civil 3D
        /// </summary>
        [CommandMethod("POINTLINE")]
        public void CreateLineFromPoints()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            try
            {
                // Mensagem inicial
                ed.WriteMessage("\n=== Civil 3D Point to Line ===");
                ed.WriteMessage("\nSelecione os pontos do Civil 3D para criar a linha.");

                // Lista para armazenar as coordenadas dos pontos selecionados
                List<Point3d> pointCoordinates = new List<Point3d>();

                // Opções de seleção para pontos do Civil 3D
                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "\nSelecione pontos do Civil 3D (ou pressione Enter para finalizar): ";
                pso.AllowDuplicates = false;
                
                // Filtrar apenas objetos do tipo CogoPoint
                TypedValue[] filterList = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.Start, "AECC_COGO_POINT")
                };
                SelectionFilter filter = new SelectionFilter(filterList);

                // Solicitar seleção dos pontos
                PromptSelectionResult psr = ed.GetSelection(pso, filter);

                if (psr.Status != PromptStatus.OK)
                {
                    ed.WriteMessage("\nSeleção cancelada ou nenhum ponto foi selecionado.");
                    return;
                }

                // Processar os pontos selecionados
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    SelectionSet ss = psr.Value;
                    
                    foreach (SelectedObject so in ss)
                    {
                        if (so != null)
                        {
                            // Obter o objeto CogoPoint
                            CogoPoint cogoPoint = tr.GetObject(so.ObjectId, OpenMode.ForRead) as CogoPoint;
                            
                            if (cogoPoint != null)
                            {
                                // Adicionar as coordenadas do ponto à lista
                                pointCoordinates.Add(cogoPoint.Location);
                                ed.WriteMessage($"\nPonto adicionado: #{cogoPoint.PointNumber} - X={cogoPoint.Easting:F3}, Y={cogoPoint.Northing:F3}, Z={cogoPoint.Elevation:F3}");
                            }
                        }
                    }

                    // Verificar se temos pelo menos 2 pontos para criar uma linha
                    if (pointCoordinates.Count < 2)
                    {
                        ed.WriteMessage("\nÉ necessário selecionar pelo menos 2 pontos para criar uma linha.");
                        return;
                    }

                    ed.WriteMessage($"\n\nTotal de pontos selecionados: {pointCoordinates.Count}");
                    ed.WriteMessage("\nCriando linha(s)...");

                    // Criar polyline conectando todos os pontos
                    Polyline pline = new Polyline();
                    
                    for (int i = 0; i < pointCoordinates.Count; i++)
                    {
                        Point3d pt = pointCoordinates[i];
                        pline.AddVertexAt(i, new Point2d(pt.X, pt.Y), 0, 0, 0);
                    }

                    // Configurar propriedades da polyline
                    pline.Elevation = pointCoordinates[0].Z;
                    pline.ColorIndex = 1; // Vermelho

                    // Adicionar a polyline ao Model Space
                    BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    
                    ObjectId lineId = btr.AppendEntity(pline);
                    tr.AddNewlyCreatedDBObject(pline, true);

                    // Calcular o comprimento total
                    double totalLength = pline.Length;
                    
                    tr.Commit();

                    ed.WriteMessage($"\n\nLinha criada com sucesso!");
                    ed.WriteMessage($"\nComprimento total: {totalLength:F3} unidades");
                    ed.WriteMessage($"\nNúmero de segmentos: {pointCoordinates.Count - 1}");
                    ed.WriteMessage("\n=== Concluído ===\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nErro ao criar linha: {ex.Message}");
            }
        }

        /// <summary>
        /// Comando alternativo que cria segmentos de linha individuais entre pontos consecutivos
        /// Uso: Digite POINTLINE_SEGMENTS no prompt de comando do Civil 3D
        /// </summary>
        [CommandMethod("POINTLINE_SEGMENTS")]
        public void CreateLineSegmentsFromPoints()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            try
            {
                ed.WriteMessage("\n=== Civil 3D Point to Line Segments ===");
                ed.WriteMessage("\nSelecione os pontos do Civil 3D para criar segmentos de linha.");

                List<Point3d> pointCoordinates = new List<Point3d>();

                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "\nSelecione pontos do Civil 3D: ";
                pso.AllowDuplicates = false;
                
                TypedValue[] filterList = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.Start, "AECC_COGO_POINT")
                };
                SelectionFilter filter = new SelectionFilter(filterList);

                PromptSelectionResult psr = ed.GetSelection(pso, filter);

                if (psr.Status != PromptStatus.OK)
                {
                    ed.WriteMessage("\nSeleção cancelada.");
                    return;
                }

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    SelectionSet ss = psr.Value;
                    
                    foreach (SelectedObject so in ss)
                    {
                        if (so != null)
                        {
                            CogoPoint cogoPoint = tr.GetObject(so.ObjectId, OpenMode.ForRead) as CogoPoint;
                            
                            if (cogoPoint != null)
                            {
                                pointCoordinates.Add(cogoPoint.Location);
                            }
                        }
                    }

                    if (pointCoordinates.Count < 2)
                    {
                        ed.WriteMessage("\nÉ necessário selecionar pelo menos 2 pontos.");
                        return;
                    }

                    BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    
                    int lineCount = 0;
                    double totalLength = 0;

                    // Criar segmentos de linha entre pontos consecutivos
                    for (int i = 0; i < pointCoordinates.Count - 1; i++)
                    {
                        Line line = new Line(pointCoordinates[i], pointCoordinates[i + 1]);
                        line.ColorIndex = 3; // Verde
                        
                        btr.AppendEntity(line);
                        tr.AddNewlyCreatedDBObject(line, true);
                        
                        totalLength += line.Length;
                        lineCount++;
                    }

                    tr.Commit();

                    ed.WriteMessage($"\n\n{lineCount} segmentos de linha criados com sucesso!");
                    ed.WriteMessage($"\nComprimento total: {totalLength:F3} unidades");
                    ed.WriteMessage("\n=== Concluído ===\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nErro: {ex.Message}");
            }
        }
    }
}
