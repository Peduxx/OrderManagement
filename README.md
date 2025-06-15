# 🧾 OrderManagement
---

## ✅ Funcionalidades principais

- Criação de pedidos com validação de estoque
- Atualização de status dos pedidos conforme fluxo permitido
- Soft delete de pedidos e itens relacionados
- Consulta de pedidos por ID ou todos os registros

---

## 🚀 Como rodar o projeto localmente

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/OrderManagement.git
cd OrderManagement
```

### 2. Subir o banco de dados e a API com Docker

```bash
docker-compose up -d
```

### 3. Aplicar migrations e criar as tabelas

Ja deixei a migration criada. Basta executar:

```bash
dotnet ef database update --project src/OrderManagement.Infra --startup-project src/OrderManagement.Api
```

---

## 🧱 Estrutura de pastas e arquitetura

O projeto segue a proposta de **Clean Architecture**, com separação clara de responsabilidades:

```
src/
├── OrderManagement.API            # Camada de apresentação (Endpoints, Middlewares, Swagger)
├── OrderManagement.Application    # Casos de uso (Handlers), validações, comandos e queries
├── OrderManagement.Domain         # Entidades, enums, interfaces e lógica de negócio
├── OrderManagement.Infra          # EF Core, Migrations e QueryServices
```

### 🔍 Decisões de arquitetura

No geral, segui as regras que já estavam descritas no documento, mas além de seguir tudo o que foi solicitado no documento, tomei algumas decisões durante o desenvolvimento que acho importantíssimo citar:

CQRS: Optei por utilizar CQRS pra dividir bem as entradas e responsabilidades no código, e embora isso não implicasse em necessariamente utilizar o MEDIATR, preferi pela facilidade que ele traz. A ideia era também trazer a visão de como poderia-se escalar a leitura de maneira isolada caso fosse necessário.

QuerySerice e não uso do repository pattern: Ainda nesse sentido, optei inicialmente por não usar repository pattern, já que o EF me oferece isso nativamente, além de UoW. Com isso, abstrair métodos simples como "Save"/"Update" apenas aumentaria a complexidade e poderia se mostrar um "anti-pattern". Com isso, optei por chamar o contexto diretamente no handler e fazer as operações de lá mesmo. Conforme a expressão de algumas queries aumentava e para atender as buscas necessárias sem repetir código, vi a necessidade de abstrair as queries, e por isso o uso do QueryService. Dessa forma eu consegui atender minhas queries sem repetir código e sem aumentar muito a complexidade.

Result Pattern: Para evitar o uso exagerado e desnecessário de Exceptions, eu optei por utilizar o Result Pattern. Por isso criei a classe Result, Error etc, minha ideia era estabelecer uma forma concisa para comunicar as camadas de Api e Application, tanto em caso de sucesso quanto em falhas já esperadas pelo sistema, que não caracterizam exceptions (email duplicado etc). Isso me ajudou a tratar os status code de maneira mais eficiente também.

Domain Exceptions: Em contrapartida, existia a necessidade de proteger o domínio de erros de programação, e então foi adicionada uma última camada de validação: a de Domain. Qualquer erro que chegasse a ocorrer no domínio, aí sim seria lançado como uma exception, pois não deveria estar acontecendo. E aí entra o próximo ponto.

Middlewares: Com o intuito de tratar tanto essas exceptions quando exceptions genéricas, criei o middleware de exceptions. Conforme surgissem outras exceptions também poderia tratar ali. E como exceptions não são "retornadas", mas sim lançadas até que encontrem uma tratativa, entendi que não havia necessidade de try catch nos handlers, pois o middleware já interceptaria. Além do ErrorMiddleware, tive o middleware de log que fiz apenas para gerar logs básicos das requisições, mostrando corpo, tempo que levou etc. Nada muito complexo e nem muito robusto.

Testes: Adicionei os testes apenas com o intuito de cumprir um dos requisitos opcionais, mas nada muito completo, apenas alguns casos mais claros.

