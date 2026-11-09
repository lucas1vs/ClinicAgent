# ClinicAgent

Automação do agendamento e do gerenciamento de consultas em clínicas e consultórios, pelo WhatsApp, usando inteligência artificial.

> 🚧 **Projeto em desenvolvimento.** Nesta primeira etapa, o foco está no backend: domínio, modelagem de dados e arquitetura. As integrações com WhatsApp, IA e Google Calendar fazem parte do roadmap.

## Ideia do projeto

Muitas clínicas dependem de uma pessoa respondendo mensagens o dia todo para marcar, remarcar e cancelar consultas. O ClinicAgent propõe outro fluxo:

1. O paciente (ou um familiar autorizado) envia uma mensagem pelo WhatsApp.
2. A IA **interpreta a mensagem** e identifica a intenção (marcar, remarcar, cancelar, consultar).
3. O **sistema aplica as regras de negócio** e é ele quem efetivamente cria ou altera o agendamento, não a IA.
4. A agenda é sincronizada com o Google Calendar.

Essa separação é proposital: a IA entende a linguagem, e as regras ficam em código, testáveis e previsíveis.

## Autorização de contatos vinculados ao paciente

Um dos pontos centrais do domínio é permitir que familiares ou responsáveis gerenciem os agendamentos de um paciente com segurança.

A entidade `PatientContactAuthorization` controla esse vínculo como uma pequena máquina de estados, com as regras de transição protegidas dentro da própria entidade:

```
          ┌── Accept() ──▶ Authorized ── Revoke() ──▶ Revoked
Pending ──┤
          └── Reject() ──▶ Rejected
```

| Transição | De | Para |
|-----------|----|------|
| `Accept()` | Pending | Authorized |
| `Reject()` | Pending | Rejected |
| `Revoke()` | Authorized | Revoked |

Qualquer transição fora dessas regras lança `InvalidOperationException`, e as datas de resposta e de revogação são registradas em UTC.

## Tecnologias

- C#
- .NET 9
- Entity Framework Core
- SQL Server

## Arquitetura

O projeto é organizado em camadas:

| Projeto | Responsabilidade |
|---------|------------------|
| `ClinicAgent` | Ponto de entrada da aplicação (API) |
| `ClinicAgent.Application` | Casos de uso, serviços e validadores |
| `ClinicAgent.Domain` | Entidades, enums, exceções e regras de negócio |
| `ClinicAgent.Infrastructure` | Acesso a dados (EF Core), configurações das entidades, repositórios e migrations |
| `ClinicAgent.Tests` | Projeto de testes |

### Entidades principais

`Patient`, `Contact`, `PatientContactAuthorization`, `Professional` e `Appointment`.

## Como executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local ou em container)
- Ferramenta do EF Core: `dotnet tool install --global dotnet-ef`

### Passo a passo

1. Clone o repositório:

   ```bash
   git clone https://github.com/lucas1vs/ClinicAgent.git
   cd ClinicAgent
   ```

2. Ajuste a connection string em `ClinicAgent/appsettings.json` para o seu SQL Server.

3. Aplique as migrations:

   ```bash
   dotnet ef database update --project ClinicAgent.Infrastructure --startup-project ClinicAgent
   ```

4. Execute a aplicação:

   ```bash
   dotnet run --project ClinicAgent
   ```

## Roadmap

- [x] Modelagem das entidades e relacionamentos
- [x] Arquitetura em camadas
- [x] Banco de dados com EF Core e SQL Server (migrations)
- [x] Fluxo de autorização de contatos vinculados ao paciente
- [ ] Endpoints da API para pacientes, profissionais e agendamentos
- [ ] Testes automatizados das regras de domínio
- [ ] Integração com o WhatsApp
- [ ] Interpretação de mensagens com IA
- [ ] Integração com o Google Calendar

## Autor

**Lucas Domingos**

- LinkedIn: [lucasdomingosdev](https://www.linkedin.com/in/lucasdomingosdev/)
- GitHub: [lucas1vs](https://github.com/lucas1vs)

Feedbacks e sugestões são bem-vindos.
