CREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(200)
);

/*

INSERT INTO Permissoes (Nome, Descricao) VALUES 
('Total', 'Acesso irrestrito a todas as funcionalidades do sistema.'),
('Limitada', 'Acesso básico a leitura de dados.');

*/


/*CREATE TABLE UsuarioPermissao (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    
    -- Tipo deve ser UNIQUEIDENTIFIER para corresponder à tabela Usuarios.Id
    UsuarioId UNIQUEIDENTIFIER NOT NULL, 
    
    PermissaoId INT NOT NULL,
    
    -- Cria as chaves estrangeiras
    CONSTRAINT FK_UsuarioPermissao_Usuario FOREIGN KEY (UsuarioId) 
        REFERENCES Usuarios (Id) ON DELETE CASCADE,
        
    CONSTRAINT FK_UsuarioPermissao_Permissao FOREIGN KEY (PermissaoId) 
        REFERENCES Permissoes (Id) ON DELETE CASCADE,
        
    -- Impede o mesmo usuário de ter a mesma permissão duas vezes
    CONSTRAINT UK_Usuario_Permissao UNIQUE (UsuarioId, PermissaoId)
);

*/