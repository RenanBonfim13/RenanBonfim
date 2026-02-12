# Installation Guide - Civil3D Point to Line Plugin

## Pré-requisitos

Antes de instalar o plugin, certifique-se de ter:

✅ AutoCAD Civil 3D 2026 instalado  
✅ .NET Framework 4.8 ou superior  
✅ Permissões de administrador (para instalação em pastas do sistema)  
✅ Visual Studio 2019+ (apenas para compilação)

---

## Opção 1: Instalar Plugin Pré-compilado

### Passo 1: Obter o arquivo DLL

Se você recebeu o arquivo `Civil3DPointLine.dll` pré-compilado, pule para o Passo 2.

Caso contrário, veja a **Opção 2** abaixo para compilar o plugin.

### Passo 2: Desbloquear o arquivo DLL

1. Clique com o botão direito no arquivo `Civil3DPointLine.dll`
2. Selecione "Propriedades"
3. Na aba "Geral", se houver uma opção "Desbloquear", marque-a
4. Clique em "OK"

### Passo 3: Carregar no Civil 3D

**Método A: Carregamento Manual (Temporário)**

1. Abra o AutoCAD Civil 3D 2026
2. Digite `NETLOAD` no prompt de comando e pressione Enter
3. Navegue até a pasta onde está `Civil3DPointLine.dll`
4. Selecione o arquivo e clique em "Abrir"
5. O plugin será carregado e estará disponível até fechar o Civil 3D

**Método B: Carregamento Automático**

1. Copie `Civil3DPointLine.dll` para uma das seguintes pastas:
   ```
   C:\Program Files\Autodesk\AutoCAD 2026\Plug-ins\
   ```
   ou
   ```
   C:\Users\[SeuUsuário]\AppData\Roaming\Autodesk\ApplicationPlugins\
   ```

2. Crie um arquivo `Civil3DPointLine.bundle` com a estrutura:
   ```
   Civil3DPointLine.bundle/
   ├── Contents/
   │   └── Civil3DPointLine.dll
   └── PackageContents.xml
   ```

3. Use o arquivo `PackageContents.xml` incluído no projeto

4. Copie a pasta `Civil3DPointLine.bundle` para:
   ```
   C:\ProgramData\Autodesk\ApplicationPlugins\
   ```

5. Reinicie o Civil 3D - o plugin será carregado automaticamente

---

## Opção 2: Compilar o Plugin do Código Fonte

### Passo 1: Verificar Instalação do Civil 3D

Certifique-se de que o Civil 3D 2026 está instalado em:
```
C:\Program Files\Autodesk\AutoCAD 2026\
```

Se estiver em outro local, você precisará atualizar as referências no arquivo `.csproj`.

### Passo 2: Abrir o Projeto

1. Abra o Visual Studio 2019 ou superior
2. Abra o arquivo `Civil3DPointLine.sln`
3. O projeto será carregado

### Passo 3: Verificar Referências

1. No Solution Explorer, expanda "Referências"
2. Verifique se todas as DLLs do Civil 3D estão com ícone de "OK"
3. Se houver erro (ícone de alerta amarelo):
   - Clique com o botão direito na referência com erro
   - Selecione "Remover"
   - Clique com botão direito em "Referências" > "Adicionar Referência"
   - Clique em "Procurar" e navegue até `C:\Program Files\Autodesk\AutoCAD 2026\`
   - Adicione as DLLs necessárias:
     - `AcCoreMgd.dll`
     - `AcDbMgd.dll`
     - `AcMgd.dll`
     - `C3D\AeccDbMgd.dll`

### Passo 4: Compilar

1. Selecione "Release" no menu dropdown de configuração
2. Clique em "Build" > "Build Solution" (ou pressione Ctrl+Shift+B)
3. Aguarde a compilação terminar
4. O arquivo `Civil3DPointLine.dll` será criado em `bin\Release\`

### Passo 5: Instalar

Siga os passos da **Opção 1, Passo 3** acima para instalar o plugin compilado.

---

## Opção 3: Usar Script de Build (PowerShell)

### Windows com PowerShell:

1. Abra PowerShell como Administrador
2. Navegue até a pasta do projeto:
   ```powershell
   cd "caminho\para\Civil3DPointLine"
   ```
3. Execute o script de build:
   ```powershell
   .\build.ps1
   ```

Se não houver script de build, você pode criar um arquivo `build.ps1`:

```powershell
# Build script para Civil3DPointLine
$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"

if (-not (Test-Path $msbuild)) {
    $msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
}

if (-not (Test-Path $msbuild)) {
    Write-Error "MSBuild não encontrado. Instale o Visual Studio."
    exit 1
}

Write-Host "Compilando Civil3DPointLine..." -ForegroundColor Green
& $msbuild "Civil3DPointLine.csproj" /p:Configuration=Release /v:minimal

if ($LASTEXITCODE -eq 0) {
    Write-Host "Compilação concluída com sucesso!" -ForegroundColor Green
    Write-Host "DLL gerada em: bin\Release\Civil3DPointLine.dll" -ForegroundColor Yellow
} else {
    Write-Error "Erro na compilação."
    exit 1
}
```

---

## Verificação da Instalação

Para verificar se o plugin foi instalado corretamente:

1. Abra o AutoCAD Civil 3D 2026
2. Digite `POINTLINE` e pressione Enter
3. Se o comando for reconhecido, o plugin está instalado corretamente!

---

## Desinstalar

### Para carregamento manual:
- Simplesmente feche o Civil 3D (o plugin não será carregado na próxima sessão)

### Para carregamento automático:
1. Feche o Civil 3D
2. Delete a pasta `Civil3DPointLine.bundle` de:
   ```
   C:\ProgramData\Autodesk\ApplicationPlugins\
   ```
3. Reinicie o Civil 3D

---

## Solução de Problemas

### Erro: "Não foi possível carregar o arquivo ou assembly"

**Causa**: Versão do .NET incorreta ou DLL bloqueada

**Solução**:
1. Certifique-se de que o .NET Framework 4.8 está instalado
2. Desbloqueie a DLL (veja Passo 2 da Opção 1)
3. Recompile o plugin com a versão correta do .NET

### Erro: "Could not load file or assembly 'AeccDbMgd'"

**Causa**: Civil 3D não está instalado ou referências incorretas

**Solução**:
1. Verifique se o Civil 3D 2026 está instalado
2. Verifique o caminho das DLLs no arquivo `.csproj`
3. Recompile o plugin

### Comando não encontrado

**Causa**: Plugin não foi carregado

**Solução**:
1. Use `NETLOAD` para carregar manualmente
2. Verifique se o arquivo DLL está na pasta correta
3. Verifique se o PackageContents.xml está configurado corretamente

### Plugin carrega mas não funciona

**Causa**: Versão do Civil 3D incompatível

**Solução**:
1. Certifique-se de que está usando o Civil 3D 2026
2. Verifique se as referências das DLLs são da versão correta
3. Recompile o plugin para a versão do seu Civil 3D

---

## Suporte Adicional

Se você ainda tiver problemas:

📧 Email: renanbonfim13@gmail.com  
🐙 GitHub: [RenanBonfim13](https://github.com/RenanBonfim13)  
💼 LinkedIn: [Renan Bonfim](https://www.linkedin.com/in/renan-b-659243133/)

---

**Desenvolvido por Renan Bonfim © 2026**
