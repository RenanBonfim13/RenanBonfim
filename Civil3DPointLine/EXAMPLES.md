# Exemplos de Uso - Civil3D Point to Line

Este documento mostra exemplos práticos de como usar o plugin Civil3D Point to Line.

## 📌 Índice

1. [Exemplo Básico - POINTLINE](#exemplo-1-criar-polyline-basico)
2. [Exemplo com Muitos Pontos](#exemplo-2-levantamento-topografico)
3. [Exemplo - POINTLINE_SEGMENTS](#exemplo-3-criar-segmentos-individuais)
4. [Caso de Uso Real - Trajetória](#exemplo-4-caso-real-trajetoria)
5. [Dicas e Truques](#dicas-e-truques)

---

## Exemplo 1: Criar Polyline Básico

### Cenário
Você tem 4 pontos topográficos e quer criar uma linha conectando-os.

### Passos

1. **Configurar pontos no Civil 3D:**
   - Ponto #1: X=100, Y=100, Z=50
   - Ponto #2: X=200, Y=150, Z=51
   - Ponto #3: X=300, Y=100, Z=49
   - Ponto #4: X=400, Y=150, Z=52

2. **Executar comando:**
   ```
   Comando: POINTLINE
   ```

3. **Selecionar pontos:**
   - Clique no Ponto #1
   - Clique no Ponto #2
   - Clique no Ponto #3
   - Clique no Ponto #4
   - Pressione Enter

4. **Resultado:**
   ```
   === Civil 3D Point to Line ===
   Selecione os pontos do Civil 3D para criar a linha.
   
   Ponto adicionado: #1 - X=100.000, Y=100.000, Z=50.000
   Ponto adicionado: #2 - X=200.000, Y=150.000, Z=51.000
   Ponto adicionado: #3 - X=300.000, Y=100.000, Z=49.000
   Ponto adicionado: #4 - X=400.000, Y=150.000, Z=52.000
   
   Total de pontos selecionados: 4
   Criando linha(s)...
   
   Linha criada com sucesso!
   Comprimento total: 447.214 unidades
   Número de segmentos: 3
   === Concluído ===
   ```

5. **O que você verá:**
   - Uma polyline vermelha conectando os 4 pontos na ordem selecionada

---

## Exemplo 2: Levantamento Topográfico

### Cenário
Você fez um levantamento topográfico de uma estrada e tem 20 pontos ao longo do eixo.

### Passos

1. **Preparar seleção:**
   - Organize os pontos em ordem (use layer ou grupo)
   - Certifique-se de que os números dos pontos estão em sequência

2. **Executar comando:**
   ```
   Comando: POINTLINE
   ```

3. **Selecionar todos os pontos:**
   - Use seleção por janela (Window)
   - Ou selecione individualmente na ordem desejada
   - Pressione Enter

4. **Resultado esperado:**
   - Uma polyline representando o eixo da estrada
   - Informações sobre comprimento total
   - Número de segmentos = número de pontos - 1

### Dica Pro
Para selecionar pontos em ordem numérica:
1. Ordene os pontos por número
2. Use filtro de seleção com ordem crescente
3. Ou selecione manualmente na ordem correta

---

## Exemplo 3: Criar Segmentos Individuais

### Cenário
Você quer analisar cada segmento separadamente.

### Passos

1. **Executar comando:**
   ```
   Comando: POINTLINE_SEGMENTS
   ```

2. **Selecionar pontos:**
   - Selecione os mesmos 4 pontos do Exemplo 1
   - Pressione Enter

3. **Resultado:**
   ```
   === Civil 3D Point to Line Segments ===
   Selecione os pontos do Civil 3D para criar segmentos de linha.
   
   3 segmentos de linha criados com sucesso!
   Comprimento total: 447.214 unidades
   === Concluído ===
   ```

4. **O que você verá:**
   - 3 linhas verdes separadas:
     - Linha 1: Ponto #1 → Ponto #2
     - Linha 2: Ponto #2 → Ponto #3
     - Linha 3: Ponto #3 → Ponto #4

### Vantagem
Cada linha é um objeto separado, facilitando:
- Edição individual
- Análise de comprimento por segmento
- Aplicação de propriedades diferentes

---

## Exemplo 4: Caso Real - Trajetória

### Cenário
Você precisa representar a trajetória de um veículo em um levantamento GPS.

### Dados
- 50 pontos GPS coletados a cada 10 segundos
- Pontos numerados de 1000 a 1049
- Coordenadas em UTM

### Workflow

1. **Importar pontos:**
   - Importe os pontos GPS para o Civil 3D
   - Verifique se são CogoPoints

2. **Filtrar pontos:**
   ```
   - Use filtro de ponto por número: 1000-1049
   - Ou por descrição: "GPS_TRAJETORIA"
   ```

3. **Criar trajetória:**
   ```
   Comando: POINTLINE
   Selecionar: Use Window Selection para pegar todos
   Enter
   ```

4. **Pós-processamento:**
   - A polyline vermelha representa a trajetória
   - Use PEDIT para suavizar se necessário
   - Analise o comprimento total percorrido

---

## Exemplo 5: Múltiplas Linhas

### Cenário
Você tem vários trechos diferentes para processar.

### Estratégia

1. **Trecho 1 - Lado Esquerdo:**
   ```
   POINTLINE → Selecionar pontos 1-10 → Enter
   ```

2. **Trecho 2 - Eixo:**
   ```
   POINTLINE → Selecionar pontos 11-20 → Enter
   ```

3. **Trecho 3 - Lado Direito:**
   ```
   POINTLINE → Selecionar pontos 21-30 → Enter
   ```

### Resultado
- 3 polylines vermelhas independentes
- Cada uma representando um trecho diferente

---

## Dicas e Truques

### ✅ Dica 1: Ordem de Seleção
A ordem em que você seleciona os pontos determina a ordem da linha!
- Para linha reta: Selecione pontos em sequência
- Para linha fechada: Selecione o primeiro ponto novamente no final

### ✅ Dica 2: Usar com Filtros
Combine com filtros do Civil 3D:
```
1. Defina um filtro de pontos (ex: Descrição = "EIXO")
2. Use POINTLINE
3. Selecione com Window/Crossing
4. Apenas pontos filtrados serão selecionados
```

### ✅ Dica 3: Edição Posterior
Após criar a polyline:
- Use PEDIT para editar vértices
- Use PROPERTIES para mudar cor/layer
- Use EXPLODE se quiser converter em linhas

### ✅ Dica 4: Salvar Seleção
Para reutilizar mesma seleção:
```
1. Crie um Selection Set
2. Use SELECTSIMILAR
3. Execute POINTLINE
```

### ✅ Dica 5: Comparar Métodos
Use ambos comandos no mesmo projeto:
- POINTLINE: Linha vermelha contínua
- POINTLINE_SEGMENTS: Linhas verdes separadas
- Compare visualmente qual método é melhor para seu caso

---

## Casos de Uso Comuns

### 🏗️ Construção Civil
- Eixos de vias
- Alinhamentos de edificações
- Limites de terreno

### 🗺️ Topografia
- Trajetórias de caminhamento
- Seções transversais
- Perfis longitudinais

### 🚧 Infraestrutura
- Redes de drenagem
- Traçados de tubulações
- Caminhos de acesso

### 📊 Análise
- Comparação de trajetórias
- Cálculo de extensões
- Verificação de alinhamentos

---

## Perguntas Frequentes

**P: Posso fechar a polyline (criar polígono)?**
R: Sim! Após criar com POINTLINE, use o comando PEDIT → Close, ou selecione o primeiro ponto novamente no final.

**P: Posso mudar a cor da linha?**
R: Sim! Use PROPERTIES ou mude a cor antes de executar o comando.

**P: Como selecionar pontos em ordem numérica?**
R: Use filtros do Civil 3D ou selecione manualmente. A ordem de seleção determina a ordem da linha.

**P: Posso usar com pontos não-consecutivos?**
R: Sim! O plugin conecta os pontos na ordem de seleção, não precisa ser consecutivo.

**P: E se eu cometer um erro na seleção?**
R: Pressione ESC durante a seleção, ou use UNDO (U) após criar a linha, e refaça.

---

## Troubleshooting

### Problema: "Nenhum ponto selecionável"
**Solução:**
- Verifique se são CogoPoints do Civil 3D
- Verifique layer ativa/congelada
- Use LIST em um ponto para verificar o tipo

### Problema: "Linha criada em local errado"
**Solução:**
- Verifique UCS ativo
- Confirme coordenadas dos pontos
- Verifique se está no Model Space

### Problema: "Comprimento não bate"
**Solução:**
- Verifique unidades do desenho
- Confirme se todos os pontos foram selecionados
- Use MEASUREGEOM para validar

---

## Suporte

📧 Email: renanbonfim13@gmail.com  
🐙 GitHub: [RenanBonfim13](https://github.com/RenanBonfim13)

---

**Desenvolvido por Renan Bonfim © 2026**
