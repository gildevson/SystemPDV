# 🔒 Esquema Completo do Sistema de Autenticação

A seguir, está o esquema completo do banco de dados para o sistema de autenticação, incluindo as tabelas de **Usuários**, **Permissões** e a tabela de relacionamento **UsuarioPermissao**, juntamente com o `INSERT` inicial de permissões.

---

## 1️⃣ Tabela `Usuarios`

Armazena as informações básicas dos usuários.

### SQL

```sql
CREATE TABLE Usuarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(500) NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT(GETDATE())
);


Chave Primária: Id (GUID/UNIQUEIDENTIFIER).

Restrição: O campo Email deve ser único.

Segurança: A senha é armazenada como SenhaHash.

2️⃣ Tabela Permissoes

Define as diferentes permissões (funções) disponíveis no sistema.

CREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(200) NULL
);


DROP TABLE IF EXISTS UsuarioPermissao;
DROP TABLE IF EXISTS Usuarios;
DROP TABLE IF EXISTS Permissoes;

CREATE TABLE Usuarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(500) NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT(GETDATE())
);

CREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(200) NULL
);

INSERT INTO Permissoes (Nome, Descricao) VALUES
('ADMIN', 'Administrador do sistema'),
('USUARIO', 'Acesso básico ao sistema');

CREATE TABLE UsuarioPermissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    PermissaoId INT NOT NULL,

    CONSTRAINT FK_UsuarioPermissao_Usuario
        FOREIGN KEY (UsuarioId)
        REFERENCES Usuarios (Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_UsuarioPermissao_Permissao
        FOREIGN KEY (PermissaoId)
        REFERENCES Permissoes (Id)
        ON DELETE CASCADE,

    CONSTRAINT UK_Usuario_Permissao UNIQUE (UsuarioId, PermissaoId)
);


Se quiser que eu adicione emojis ou comentários adicionais no código, posso ajustar. Deseja isso?

