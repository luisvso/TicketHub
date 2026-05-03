-- Setores
INSERT INTO "Setores" ("Nome", "Descricao") VALUES
('TI', 'Setor responsável pela infraestrutura e sistemas'),
('Suporte', 'Atendimento e suporte aos colaboradores'),
('Financeiro', 'Gestão financeira e contabilidade'),
('RH', 'Recursos humanos e gestão de pessoas'),
('Comercial', 'Vendas e relacionamento com clientes')
ON CONFLICT DO NOTHING;

-- Prioridades
INSERT INTO "Prioridades" ("Nome", "HorasEstimadas") VALUES
('Baixa',   8),
('Média',   4),
('Alta',    2),
('Crítica', 1)
ON CONFLICT DO NOTHING;

-- Chamados
-- 1. Aberto
INSERT INTO "Chamados" ("Titulo", "Descricao", "Status", "DataAbertura", "DataInicio", "DataFinal", "Solucao", "SetorId", "PrioridadeId")
VALUES ('Sem acesso ao sistema', 'Usuário não consegue logar no sistema interno.', 0, NOW(), NULL, NULL, NULL, 1, 2);

-- 2. Em Atendimento — dentro do prazo
INSERT INTO "Chamados" ("Titulo", "Descricao", "Status", "DataAbertura", "DataInicio", "DataFinal", "Solucao", "SetorId", "PrioridadeId")
VALUES ('Impressora offline', 'Impressora do setor financeiro não responde.', 1, NOW() - INTERVAL '1 hour', NOW() - INTERVAL '30 minutes', NULL, NULL, 3, 1);

-- 3. Em Atendimento — ATRASADO
INSERT INTO "Chamados" ("Titulo", "Descricao", "Status", "DataAbertura", "DataInicio", "DataFinal", "Solucao", "SetorId", "PrioridadeId")
VALUES ('Servidor fora do ar', 'Servidor principal parou de responder.', 1, NOW() - INTERVAL '5 hours', NOW() - INTERVAL '3 hours', NULL, NULL, 1, 4);

-- 4. Finalizado
INSERT INTO "Chamados" ("Titulo", "Descricao", "Status", "DataAbertura", "DataInicio", "DataFinal", "Solucao", "SetorId", "PrioridadeId")
VALUES ('Troca de mouse', 'Mouse do colaborador parou de funcionar.', 2, NOW() - INTERVAL '2 days', NOW() - INTERVAL '2 days' + INTERVAL '1 hour', NOW() - INTERVAL '2 days' + INTERVAL '3 hours', 'Mouse substituído por um reserva do estoque.', 2, 1);

-- 5. Cancelado
INSERT INTO "Chamados" ("Titulo", "Descricao", "Status", "DataAbertura", "DataInicio", "DataFinal", "Solucao", "SetorId", "PrioridadeId")
VALUES ('Solicitação duplicada', 'Chamado aberto em duplicidade pelo usuário.', 3, NOW() - INTERVAL '1 day', NULL, NULL, NULL, 4, 2);