
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/06/2020 00:55:33
-- Generated from EDMX file: C:\Users\RICARDO\Documents\Code_Estudos\01_Certificado_70-483\03_Acesso_dados\Exemplos\2_Consume\EF_Model_First\EF_Model_First\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [School];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EstudanteSet'
CREATE TABLE [dbo].[EstudanteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Sobrenome] nvarchar(max)  NOT NULL,
    [Nascimento] datetime  NOT NULL
);
GO

-- Creating table 'CursoSet'
CREATE TABLE [dbo].[CursoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NomeCurso] nvarchar(max)  NOT NULL,
    [IdEstudante] int  NOT NULL
);
GO

-- Creating table 'CursoEstudante'
CREATE TABLE [dbo].[CursoEstudante] (
    [Curso_Id] int  NOT NULL,
    [Estudante_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EstudanteSet'
ALTER TABLE [dbo].[EstudanteSet]
ADD CONSTRAINT [PK_EstudanteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CursoSet'
ALTER TABLE [dbo].[CursoSet]
ADD CONSTRAINT [PK_CursoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Curso_Id], [Estudante_Id] in table 'CursoEstudante'
ALTER TABLE [dbo].[CursoEstudante]
ADD CONSTRAINT [PK_CursoEstudante]
    PRIMARY KEY CLUSTERED ([Curso_Id], [Estudante_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Curso_Id] in table 'CursoEstudante'
ALTER TABLE [dbo].[CursoEstudante]
ADD CONSTRAINT [FK_CursoEstudante_Curso]
    FOREIGN KEY ([Curso_Id])
    REFERENCES [dbo].[CursoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Estudante_Id] in table 'CursoEstudante'
ALTER TABLE [dbo].[CursoEstudante]
ADD CONSTRAINT [FK_CursoEstudante_Estudante]
    FOREIGN KEY ([Estudante_Id])
    REFERENCES [dbo].[EstudanteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CursoEstudante_Estudante'
CREATE INDEX [IX_FK_CursoEstudante_Estudante]
ON [dbo].[CursoEstudante]
    ([Estudante_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------