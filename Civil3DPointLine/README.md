# Civil3D Point to Line Plugin

Plugin para AutoCAD Civil 3D 2026 que permite selecionar vários pontos (CogoPoints) e criar linhas conectando-os.

## Descrição

Este plugin adiciona comandos ao AutoCAD Civil 3D 2026 que facilitam a criação de linhas a partir de pontos topográficos. É útil para:
- Conectar pontos de levantamento topográfico
- Criar representações lineares de trajetórias
- Gerar polilíneas a partir de pontos COGO

## Funcionalidades

### Comando POINTLINE
Cria uma polyline contínua conectando todos os pontos selecionados na ordem em que foram selecionados.

**Características:**
- Conecta múltiplos pontos em uma única polyline
- Cor: Vermelho (colorIndex 1)
- Mantém a elevação do primeiro ponto
- Mostra comprimento total e número de segmentos

### Comando POINTLINE_SEGMENTS
Cria segmentos de linha individuais entre pontos consecutivos.

**Características:**
- Cria linhas separadas entre cada par de pontos consecutivos
- Cor: Verde (colorIndex 3)
- Mostra comprimento total e número de segmentos criados

## Requisitos

- AutoCAD Civil 3D 2026
- .NET Framework 4.8
- Sistema operacional Windows

## Instalação

### Método 1: Instalação Manual

1. **Compilar o plugin:**
   - Abra o projeto `Civil3DPointLine.csproj` no Visual Studio
   - Certifique-se de que os caminhos das referências do Civil 3D estão corretos
   - Compile o projeto em modo Release
   - O arquivo `Civil3DPointLine.dll` será gerado na pasta `bin\Release`

2. **Carregar no Civil 3D:**
   - Abra o AutoCAD Civil 3D 2026
   - Digite `NETLOAD` no prompt de comando
   - Navegue até o arquivo `Civil3DPointLine.dll`
   - Selecione o arquivo e clique em "Abrir"

### Método 2: Carregamento Automático

1. Copie o arquivo `Civil3DPointLine.dll` para uma das seguintes pastas:
   - `C:\Program Files\Autodesk\AutoCAD 2026\C3D\Plug-ins`
   - Ou crie uma pasta personalizada e adicione ao caminho de suporte do Civil 3D

2. Crie um arquivo `Civil3DPointLine.bundle` com a seguinte estrutura:
   ```
   Civil3DPointLine.bundle/
   ├── Contents/
   │   └── Civil3DPointLine.dll
   └── PackageContents.xml
   ```

3. Crie o arquivo `PackageContents.xml`:
   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <ApplicationPackage>
     <Components>
       <RuntimeRequirements OS="Win64" Platform="AutoCAD"/>
       <ComponentEntry AppName="Civil3DPointLine" Version="1.0.0" ModuleName="./Contents/Civil3DPointLine.dll"/>
     </Components>
   </ApplicationPackage>
   ```

## Uso

### Usando o comando POINTLINE

1. Abra um desenho no Civil 3D 2026 com pontos COGO
2. Digite `POINTLINE` no prompt de comando e pressione Enter
3. Selecione os pontos desejados (na ordem desejada)
4. Pressione Enter para finalizar a seleção
5. Uma polyline vermelha será criada conectando os pontos

**Exemplo de saída:**
```
=== Civil 3D Point to Line ===
Selecione os pontos do Civil 3D para criar a linha.
Ponto adicionado: #1 - X=100.000, Y=200.000, Z=50.000
Ponto adicionado: #2 - X=150.000, Y=250.000, Z=51.000
Ponto adicionado: #3 - X=200.000, Y=200.000, Z=49.000

Total de pontos selecionados: 3
Criando linha(s)...

Linha criada com sucesso!
Comprimento total: 141.421 unidades
Número de segmentos: 2
=== Concluído ===
```

### Usando o comando POINTLINE_SEGMENTS

1. Digite `POINTLINE_SEGMENTS` no prompt de comando
2. Selecione os pontos desejados
3. Pressione Enter para finalizar
4. Segmentos de linha verdes serão criados entre pontos consecutivos

## Desenvolvimento

### Estrutura do Projeto

```
Civil3DPointLine/
├── Civil3DPointLine.csproj          # Arquivo de projeto
├── PointLineCommands.cs              # Classe principal com comandos
└── Properties/
    └── AssemblyInfo.cs               # Informações do assembly
```

### Compilação

```bash
# Usando dotnet CLI (se disponível)
dotnet build Civil3DPointLine.csproj -c Release

# Ou abra o projeto no Visual Studio e compile normalmente
```

### Requisitos de Desenvolvimento

- Visual Studio 2019 ou superior
- .NET Framework 4.8 SDK
- AutoCAD Civil 3D 2026 SDK

### Configuração de Referências

As seguintes DLLs do Civil 3D devem estar referenciadas:
- `AcCoreMgd.dll` - Core AutoCAD API
- `AcDbMgd.dll` - Database API
- `AcMgd.dll` - Application API
- `AeccDbMgd.dll` - Civil 3D API

Caminho padrão: `C:\Program Files\Autodesk\AutoCAD 2026\C3D\`

## Solução de Problemas

### Plugin não carrega
- Verifique se o Civil 3D 2026 está instalado
- Certifique-se de que o .NET Framework 4.8 está instalado
- Verifique se o arquivo DLL não está bloqueado (clique com botão direito > Propriedades > Desbloquear)

### Comando não encontrado
- Recarregue o plugin com `NETLOAD`
- Verifique se o comando está digitado corretamente (POINTLINE ou POINTLINE_SEGMENTS)

### Nenhum ponto selecionável
- Certifique-se de que existem objetos CogoPoint no desenho
- Verifique se os pontos não estão em uma layer congelada ou desligada

### Erro ao criar linha
- Verifique se há espaço suficiente no Model Space
- Certifique-se de que os pontos têm coordenadas válidas

## Licença

Copyright © 2026 Renan Bonfim

## Autor

**Renan Bonfim**
- Email: renanbonfim13@gmail.com
- GitHub: [@RenanBonfim13](https://github.com/RenanBonfim13)
- LinkedIn: [Renan Bonfim](https://www.linkedin.com/in/renan-b-659243133/)

## Versão

**1.0.0** - Versão inicial
- Comando POINTLINE para criar polyline
- Comando POINTLINE_SEGMENTS para criar segmentos individuais
- Suporte para pontos COGO do Civil 3D 2026

## Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para:
- Reportar bugs
- Sugerir novas funcionalidades
- Enviar pull requests

## Changelog

### [1.0.0] - 2026-02-12
- Versão inicial do plugin
- Implementação do comando POINTLINE
- Implementação do comando POINTLINE_SEGMENTS
- Suporte para seleção múltipla de pontos COGO
- Exibição de informações detalhadas (coordenadas, comprimento, etc.)
