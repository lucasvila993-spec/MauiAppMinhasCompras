# MauiAppMinhasCompras

Projeto .NET MAUI com SQLite e CRUD, baseado nas três agendas de Desenvolvimento Mobile enviadas.

## Requisitos
- Visual Studio 2022 com a carga de trabalho .NET MAUI instalada.
- .NET 8 SDK/MAUI.
- Para Android: Android SDK/emulador configurado no Visual Studio.
- Para Windows: workload de desenvolvimento para .NET MAUI/Windows.

## Como executar
1. Abra `MauiAppMinhasCompras.sln` no Visual Studio.
2. Aguarde a restauração dos pacotes NuGet.
3. Se necessário, aceite a instalação/restauração do Android SDK.
4. Escolha `Windows Machine` ou um emulador/dispositivo Android.
5. Pressione F5.

## Funcionalidades
- Cadastrar produto
- Listar produtos
- Pesquisar produtos
- Editar produto
- Excluir produto
- Persistência local em SQLite

O banco é criado automaticamente como `banco_sqlite_compras.db3` no diretório de dados locais do aplicativo.
