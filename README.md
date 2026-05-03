# TicketHub 🎫
 
Sistema de controle de atendimentos e chamados internos desenvolvido em **ASP.NET Core** com **PostgreSQL**.
 
## Funcionalidades
 
- Cadastro de Setores
- Cadastro de Prioridades com tempo estimado em horas
- Abertura de Chamados vinculados a Setor e Prioridade
- Check-in: registro de início do atendimento
- Check-out: registro de término com solução
- Listagem com filtros por Setor, Prioridade e Status
- Destaque automático de chamados atrasados
## Requisitos
 
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/)
## Como rodar
 
### 1. Clone o repositório
 
```bash
git clone https://github.com/luisvso/TicketHub.git
cd TicketHub
```
 
### 2. Configure as variáveis de ambiente
 
```bash
cp .env_template .env
```
 
Abra o arquivo `.env` e preencha com suas credenciais:
 
```env
DB_NAME=ticketDB
DB_USER=seu_usuario
DB_PASSWORD=sua_senha
DB_HOST=db_ticket_hub
DB_PORT=5432
```
 
### 3. Suba a aplicação
 
```bash
docker-compose up --build
```
 
Isso irá:
- Subir o banco de dados PostgreSQL
- Buildar e subir a aplicação
- Rodar as migrations automaticamente
- Popular o banco com dados iniciais
### 4. Acesse no navegador
 
```
http://localhost:8080
```
 
## Dados iniciais
 
Ao subir a aplicação, o banco é populado automaticamente com:
 
**Setores:** TI, Suporte, Financeiro, RH, Comercial
 
**Prioridades:**
| Nome | Tempo Estimado |
|---|---|
| Baixa | 8 horas |
| Média | 4 horas |
| Alta | 2 horas |
| Crítica | 1 hora |
 
**Chamados de exemplo:**
| Chamado | Status |
|---|---|
| Sem acesso ao sistema | Aberto |
| Impressora offline | Em Atendimento |
| Servidor fora do ar | Em Atendimento (Atrasado) |
| Troca de mouse | Finalizado |
| Solicitação duplicada | Cancelado |
 
## Parar a aplicação
 
```bash
docker-compose down
```
 
Para parar e remover os dados do banco:
 
```bash
docker-compose down -v
```
 
