# Comandos e passos para migrações (Entity Framework Core)

Este arquivo explica, passo a passo, como criar e aplicar migrações usando o EF Core. Instruções pensadas para uso no Visual Studio (__Abrir no Terminal__) ou via linha de comando. Assuma que o projeto alvo é MeuCorre.Infra e que a aplicação de inicialização (startup) é MeuCorre.

## Pré-requisitos
- .NET SDK (compatível com .NET 8) instalado.
- Pacote NuGet `Microsoft.EntityFrameworkCore.Design` instalado no projeto que contém o DbContext.
- (Opcional) Ferramenta global CLI `dotnet-ef` instalada: `dotnet tool install --global dotnet-ef` ou use o `dotnet ef` fornecido pelo SDK.
- Verifique a string de conexão e o DbContext corretamente registrados em MeuCorre.Infra/MeuDbContext.cs.
- Compile a solução antes de criar migração: `dotnet build` ou __Build__ no Visual Studio.

---

## 1) Abrir o terminal no projeto correto
No Visual Studio:
- Clique com o botão direito no projeto MeuCorre.Infra e selecione __Abrir no Terminal__.
Ou abra um terminal no diretório do projeto MeuCorre.Infra (pasta que contém o arquivo .csproj do projeto de infra).

---

## 2) Adicionar uma nova migração
Recomendações de nome: use PascalCase e descreva a mudança (ex.: `AddUsuarioIsActive`, `RemoveEnderecoCampoX`, `Ticket1234_AddIndex`).

Com o terminal aberto no diretório do projeto que contém o DbContext:
`dotnet ef migrations add NomeDaMigracao`

Exemplo:
`dotnet ef migrations add AddUsuarioIsActive`

Caso queira adicionar uma descrição à migração, acrescente um hífen e a descrição após o nome da migração.
Exemplo: `dotnet ef migrations add AddUsuarioIsActive-AddCampoIsActive`

**Atenção:** Se houver mais de um projeto na solução, é necessário especificar o projeto de startup (normalmente MeuCorre) com a opção `--startup-project` se o mesmo não estiver definido como projeto de inicialização padrão. Exemplo:
`dotnet ef migrations add NomeDaMigracao --startup-project ../caminho/do/projeto/MeuCorre`

Se precisar especificar projeto e startup:
```bash
dotnet ef migrations add NomeDaMigracao --project MeuCorre.Infra --startup-project MeuCorre
```

- -p / --project: projeto que contém as migrações (onde fica o DbContext).
- -s / --startup-project: projeto que contém a configuração de inicialização (ex.: Program.cs).

Se houver múltiplos DbContext, especifique o nome do contexto:
---

## 3) Aplicar (atualizar) o banco de dados
Com o terminal ainda no diretório apropriado:
`dotnet ef database update`

Esse comando aplica todas as migrações pendentes para o banco de dados. Durante a atualização, o EF Core executa as operações necessárias para trazer o banco de dados para o estado mais recente conforme definido pelas migrações.

---

## 4) Reverter a última migração (se necessário)
Caso precise desfazer a última migração aplicada, utilize o comando:
`dotnet ef database update ÚltimaMigraçãoAntesDaQueDesejaReverter`

Exemplo:
`dotnet ef database update 20230315_InitialCreate`

Em que `20230315_InitialCreate` seria o nome da migração que está imediatamente antes da migração que você deseja reverter.

---

## 5) Dicas e resolução de problemas
- Se o comando não for reconhecido, verifique se o `dotnet-ef` está instalado ou se o pacote `Microsoft.EntityFrameworkCore.Design` está presente.
- Se a migração não reflete mudanças esperadas, confirme que as classes de entidade e o DbContext foram salvas e a solução foi compilada.
- Use `dotnet ef migrations list` para ver migrações existentes.
- Para reverter a última aplicação: `dotnet ef database update NomeDaMigracaoAnterior`.
- Mantenha as migrações sob controle de versão (commitar a pasta Migrations).

---

Boas práticas: crie migrações pequenas e atômicas, documente o propósito no nome e no commit, e sempre teste as migrações em um ambiente local antes de aplicar em produção.

---

## 6) Problemas Comuns
- Se ao rodar `dotnet ef` ocorrer um erro dizendo que o comando não foi encontrado, verifique se o `dotnet-ef` está instalado corretamente ou se o caminho para o mesmo está adicionado nas variáveis de ambiente do sistema.
- Erros de conexão com o banco de dados normalmente estão relacionados à string de conexão. Verifique se as credenciais, o servidor e o banco de dados estão corretos e acessíveis.

---

## 7) Documentação Adicional
Para mais detalhes sobre as alterações em massa (bulk changes) e outras funcionalidades avançadas, consulte a [documentação oficial do EF Core](https://docs.microsoft.com/ef/core/cli/dotnet).