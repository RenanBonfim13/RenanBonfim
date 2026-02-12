# Guia Rápido - Civil3D Point to Line

## 📋 O que faz este plugin?

Este plugin permite criar linhas automaticamente a partir de pontos topográficos (CogoPoints) no AutoCAD Civil 3D 2026.

## 🚀 Como usar

### Passo 1: Carregar o Plugin

1. Abra o AutoCAD Civil 3D 2026
2. Digite `NETLOAD` e pressione Enter
3. Navegue até a pasta onde está o arquivo `Civil3DPointLine.dll`
4. Selecione o arquivo e clique em "Abrir"

### Passo 2: Criar uma Linha

**Opção A: Criar Polyline Contínua**
1. Digite `POINTLINE` e pressione Enter
2. Selecione os pontos que deseja conectar (clique em cada ponto)
3. Pressione Enter quando terminar de selecionar
4. Uma linha vermelha será criada conectando todos os pontos!

**Opção B: Criar Segmentos Individuais**
1. Digite `POINTLINE_SEGMENTS` e pressione Enter
2. Selecione os pontos desejados
3. Pressione Enter quando terminar
4. Segmentos de linha verdes serão criados entre cada par de pontos consecutivos!

## 💡 Dicas

- **Ordem importa**: Os pontos serão conectados na ordem em que você selecioná-los
- **Mínimo de pontos**: Você precisa selecionar pelo menos 2 pontos
- **Tipo de objeto**: O plugin só funciona com pontos COGO do Civil 3D
- **Informações**: O plugin mostra o comprimento total e número de segmentos criados

## 🎨 Diferenças entre os comandos

| Comando | Resultado | Cor |
|---------|-----------|-----|
| `POINTLINE` | Uma polyline contínua | Vermelho |
| `POINTLINE_SEGMENTS` | Linhas separadas | Verde |

## 📝 Exemplo de Uso

```
Comando: POINTLINE
=== Civil 3D Point to Line ===
Selecione os pontos do Civil 3D para criar a linha.
Selecione pontos: [clique no ponto 1]
Selecione pontos: [clique no ponto 2]
Selecione pontos: [clique no ponto 3]
Selecione pontos: [pressione Enter]

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

## ❓ Problemas Comuns

**"Seleção cancelada ou nenhum ponto foi selecionado"**
- Certifique-se de que está selecionando pontos COGO do Civil 3D
- Verifique se os pontos não estão em uma layer desligada

**"É necessário selecionar pelo menos 2 pontos"**
- Você precisa selecionar no mínimo 2 pontos para criar uma linha
- Tente selecionar mais pontos

**Comando não encontrado**
- Verifique se o plugin foi carregado corretamente com NETLOAD
- Digite o comando em MAIÚSCULAS: `POINTLINE`

## 📞 Suporte

Se precisar de ajuda, entre em contato:
- Email: renanbonfim13@gmail.com
- GitHub: github.com/RenanBonfim13

## 🎯 Versão

**v1.0.0** - Versão inicial

---

Desenvolvido por Renan Bonfim © 2026
