-- ============================================================
-- Script de creacion de base de datos DB_Medicamentos
-- Sistema de Gestion de Medicamentos
-- ============================================================

-- Crear la base de datos
CREATE DATABASE DB_Medicamentos;
GO

USE DB_Medicamentos;
GO

-- ============================================================
-- TABLA: Medicamentos
-- ============================================================
CREATE TABLE Medicamentos
(
    IdMedicamento    INT           IDENTITY(1,1) PRIMARY KEY,
    Nombre           NVARCHAR(100) NOT NULL,
    Categoria        NVARCHAR(100) NOT NULL,
    Cantidad         INT           NOT NULL,
    FechaVencimiento DATE          NOT NULL,
    Descripcion      NVARCHAR(250) NULL
);
GO

-- ============================================================
-- INSERTS: 10 medicamentos de ejemplo
-- ============================================================
INSERT INTO Medicamentos (Nombre, Categoria, Cantidad, FechaVencimiento, Descripcion)
VALUES
('Acetaminofen',       'Analgesicos',        50, '2026-12-31', 'Alivia el dolor leve a moderado'),
('Ibuprofeno',         'Antiinflamatorios',  30, '2026-11-15', 'Antiinflamatorio no esteroideo'),
('Amoxicilina',        'Antibioticos',       20, '2026-10-01', 'Antibiotico de amplio espectro'),
('Loratadina',         'Antihistaminicos',   15, '2026-09-20', 'Para alergias estacionales'),
('Omeprazol',          'Gastrointestinales',  40, '2026-08-10', 'Inhibidor de bomba de protones'),
('Metformina',         'Antidiabeticos',      25, '2026-12-01', 'Control de glucosa en sangre'),
('Losartan',           'Antihipertensivos',   35, '2027-01-15', 'Bloqueador de receptores de angiotensina'),
('Salbutamol',         'Broncodilatadores',    8, '2026-07-30', 'Para asma y EPOC'),
('Diazepam',           'Ansioliticos',         5, '2026-06-15', 'Ansiolitico y relajante muscular'),
('Cetirizina',         'Antihistaminicos',   12, '2026-10-05', 'Antihistaminico de segunda generacion');
GO
