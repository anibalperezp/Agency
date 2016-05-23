
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/19/2015 23:35:58
-- Generated from EDMX file: C:\Users\Lenovo\Documents\Visual Studio 2013\Projects\TripAgency\TripAgency\EF\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TripAgency];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Avion_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Avion] DROP CONSTRAINT [FK_Avion_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Avion_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Avion] DROP CONSTRAINT [FK_Avion_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Carros_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Carro] DROP CONSTRAINT [FK_Carros_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Carros_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Carro] DROP CONSTRAINT [FK_Carros_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_ClaseCatTarAvi_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomClaseCatTarAvi] DROP CONSTRAINT [FK_ClaseCatTarAvi_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_ClaseCatTarAvi_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomClaseCatTarAvi] DROP CONSTRAINT [FK_ClaseCatTarAvi_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Excursion_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Excursion] DROP CONSTRAINT [FK_Excursion_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Excursion_ToTable2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Excursion] DROP CONSTRAINT [FK_Excursion_ToTable2];
GO
IF OBJECT_ID(N'[dbo].[FK_Hotel_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Hotel] DROP CONSTRAINT [FK_Hotel_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Hotel_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Hotel] DROP CONSTRAINT [FK_Hotel_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Hotel_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Hotel] DROP CONSTRAINT [FK_Hotel_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temp_Hotel_ToTable_6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempSupHotel] DROP CONSTRAINT [FK_Nom_Temp_Hotel_ToTable_6];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temp_Hotel_ToTable_7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempSupHotel] DROP CONSTRAINT [FK_Nom_Temp_Hotel_ToTable_7];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temp_Hotel_ToTable_8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempSupHotel] DROP CONSTRAINT [FK_Nom_Temp_Hotel_ToTable_8];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temporada_Hotel_ToTable_]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempRedHotel] DROP CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temporada_Hotel_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempRedHotel] DROP CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temporada_Hotel_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempRedHotel] DROP CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temporada_Hotel_ToTable_6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempTipHotel] DROP CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_6];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temporada_Hotel_ToTable_7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempTipHotel] DROP CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_7];
GO
IF OBJECT_ID(N'[dbo].[FK_Nom_Temporada_Hotel_ToTable_8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempTipHotel] DROP CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_8];
GO
IF OBJECT_ID(N'[dbo].[FK_NomAvionClase_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomAvionClase] DROP CONSTRAINT [FK_NomAvionClase_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_NomAvionClase_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomAvionClase] DROP CONSTRAINT [FK_NomAvionClase_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_NomTempClase_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempAvionClase] DROP CONSTRAINT [FK_NomTempClase_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_NomTempClase_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempAvionClase] DROP CONSTRAINT [FK_NomTempClase_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_NomTempClase_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NomTempAvionClase] DROP CONSTRAINT [FK_NomTempClase_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_OficinaRenta_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OficinaRenta] DROP CONSTRAINT [FK_OficinaRenta_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_OficinaRenta_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OficinaRenta] DROP CONSTRAINT [FK_OficinaRenta_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_OficPerCar_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OficPerCar] DROP CONSTRAINT [FK_OficPerCar_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_OficPerCar_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OficPerCar] DROP CONSTRAINT [FK_OficPerCar_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Paquete_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paquete] DROP CONSTRAINT [FK_Paquete_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Rental_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rental] DROP CONSTRAINT [FK_Rental_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Rental_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rental] DROP CONSTRAINT [FK_Rental_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Rental_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rental] DROP CONSTRAINT [FK_Rental_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_Subdestino_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subdestino] DROP CONSTRAINT [FK_Subdestino_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Table_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Empresa_TipoEmpresa] DROP CONSTRAINT [FK_Table_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Table_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Empresa_TipoEmpresa] DROP CONSTRAINT [FK_Table_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_TarifaCarro_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TarifaCarro] DROP CONSTRAINT [FK_TarifaCarro_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_TarifaCarro_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TarifaCarro] DROP CONSTRAINT [FK_TarifaCarro_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_TarifaCarro_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TarifaCarro] DROP CONSTRAINT [FK_TarifaCarro_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_TarifaCarro_ToTable_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TarifaCarro] DROP CONSTRAINT [FK_TarifaCarro_ToTable_5];
GO
IF OBJECT_ID(N'[dbo].[FK_TarifaCarro_ToTable_7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TarifaCarro] DROP CONSTRAINT [FK_TarifaCarro_ToTable_7];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Avion_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaAvion] DROP CONSTRAINT [FK_Temporada_Avion_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Avion_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaAvion] DROP CONSTRAINT [FK_Temporada_Avion_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Avion_ToTable_4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaAvion] DROP CONSTRAINT [FK_Temporada_Avion_ToTable_4];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Avion_ToTable_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaAvion] DROP CONSTRAINT [FK_Temporada_Avion_ToTable_5];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Excursion_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaExcursion] DROP CONSTRAINT [FK_Temporada_Excursion_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Excursion_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaExcursion] DROP CONSTRAINT [FK_Temporada_Excursion_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Excursion_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaExcursion] DROP CONSTRAINT [FK_Temporada_Excursion_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Excursion_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaExcursion] DROP CONSTRAINT [FK_Temporada_Excursion_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Hotel_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Hotel] DROP CONSTRAINT [FK_Temporada_Hotel_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Hotel_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Hotel] DROP CONSTRAINT [FK_Temporada_Hotel_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Hotel_ToTable_4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Hotel] DROP CONSTRAINT [FK_Temporada_Hotel_ToTable_4];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Hotel_ToTable_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Hotel] DROP CONSTRAINT [FK_Temporada_Hotel_ToTable_5];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Paquete_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaPaquete] DROP CONSTRAINT [FK_Temporada_Paquete_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Paquete_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaPaquete] DROP CONSTRAINT [FK_Temporada_Paquete_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Paquete_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaPaquete] DROP CONSTRAINT [FK_Temporada_Paquete_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Paquete_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaPaquete] DROP CONSTRAINT [FK_Temporada_Paquete_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Reduccion_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Reduccion] DROP CONSTRAINT [FK_Temporada_Reduccion_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Reduccion_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Reduccion] DROP CONSTRAINT [FK_Temporada_Reduccion_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Reduccion_ToTable_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Reduccion] DROP CONSTRAINT [FK_Temporada_Reduccion_ToTable_5];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Reduccion_ToTable_6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Reduccion] DROP CONSTRAINT [FK_Temporada_Reduccion_ToTable_6];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Suplemento_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Suplemento] DROP CONSTRAINT [FK_Temporada_Suplemento_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Suplemento_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Suplemento] DROP CONSTRAINT [FK_Temporada_Suplemento_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Suplemento_ToTable_4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Suplemento] DROP CONSTRAINT [FK_Temporada_Suplemento_ToTable_4];
GO
IF OBJECT_ID(N'[dbo].[FK_Temporada_Suplemento_ToTable_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Temporada_Suplemento] DROP CONSTRAINT [FK_Temporada_Suplemento_ToTable_5];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaEmpresa_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaEmpresa] DROP CONSTRAINT [FK_TemporadaEmpresa_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaEmpresa_ToTable_4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaEmpresa] DROP CONSTRAINT [FK_TemporadaEmpresa_ToTable_4];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaEmpresa_ToTable_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaEmpresa] DROP CONSTRAINT [FK_TemporadaEmpresa_ToTable_5];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaEmpresa_ToTable_7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaEmpresa] DROP CONSTRAINT [FK_TemporadaEmpresa_ToTable_7];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaRental_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaRental] DROP CONSTRAINT [FK_TemporadaRental_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaRental_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaRental] DROP CONSTRAINT [FK_TemporadaRental_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaRental_ToTable_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaRental] DROP CONSTRAINT [FK_TemporadaRental_ToTable_2];
GO
IF OBJECT_ID(N'[dbo].[FK_TemporadaRental_ToTable_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemporadaRental] DROP CONSTRAINT [FK_TemporadaRental_ToTable_3];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_HotelTipol_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tipol_HotelTipol] DROP CONSTRAINT [FK_Tipol_HotelTipol_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_HotelTipol_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tipol_HotelTipol] DROP CONSTRAINT [FK_Tipol_HotelTipol_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_RangosTarifaCarro_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rangos_Tarifa] DROP CONSTRAINT [FK_Tipol_RangosTarifaCarro_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_RangosTarifaCarro_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rangos_Tarifa] DROP CONSTRAINT [FK_Tipol_RangosTarifaCarro_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_ReduccTipol_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reduccion_Hotel] DROP CONSTRAINT [FK_Tipol_ReduccTipol_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_ReduccTipol_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reduccion_Hotel] DROP CONSTRAINT [FK_Tipol_ReduccTipol_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_SupleTipol_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Suplemento_Hotel] DROP CONSTRAINT [FK_Tipol_SupleTipol_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipol_SupleTipol_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Suplemento_Hotel] DROP CONSTRAINT [FK_Tipol_SupleTipol_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Tipologia_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tipologia] DROP CONSTRAINT [FK_Tipologia_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Vuelo_ToTable2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Avion] DROP CONSTRAINT [FK_Vuelo_ToTable2];
GO
IF OBJECT_ID(N'[dbo].[FK_Vuelo_ToTable4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Avion] DROP CONSTRAINT [FK_Vuelo_ToTable4];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Anno]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Anno];
GO
IF OBJECT_ID(N'[dbo].[Avion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Avion];
GO
IF OBJECT_ID(N'[dbo].[Cadena]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cadena];
GO
IF OBJECT_ID(N'[dbo].[Carro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Carro];
GO
IF OBJECT_ID(N'[dbo].[Categoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categoria];
GO
IF OBJECT_ID(N'[dbo].[CategTarifAvion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategTarifAvion];
GO
IF OBJECT_ID(N'[dbo].[Clase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clase];
GO
IF OBJECT_ID(N'[dbo].[Destino]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Destino];
GO
IF OBJECT_ID(N'[dbo].[DestinoRental]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DestinoRental];
GO
IF OBJECT_ID(N'[dbo].[DestinoVuelo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DestinoVuelo];
GO
IF OBJECT_ID(N'[dbo].[Empresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empresa];
GO
IF OBJECT_ID(N'[dbo].[Empresa_TipoEmpresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empresa_TipoEmpresa];
GO
IF OBJECT_ID(N'[dbo].[Estacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Estacion];
GO
IF OBJECT_ID(N'[dbo].[Excursion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Excursion];
GO
IF OBJECT_ID(N'[dbo].[Hotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hotel];
GO
IF OBJECT_ID(N'[dbo].[NomAvionClase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NomAvionClase];
GO
IF OBJECT_ID(N'[dbo].[NomClaseCatTarAvi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NomClaseCatTarAvi];
GO
IF OBJECT_ID(N'[dbo].[NomTempAvionClase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NomTempAvionClase];
GO
IF OBJECT_ID(N'[dbo].[NomTempRedHotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NomTempRedHotel];
GO
IF OBJECT_ID(N'[dbo].[NomTempSupHotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NomTempSupHotel];
GO
IF OBJECT_ID(N'[dbo].[NomTempTipHotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NomTempTipHotel];
GO
IF OBJECT_ID(N'[dbo].[OficinaRenta]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OficinaRenta];
GO
IF OBJECT_ID(N'[dbo].[OficPerCar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OficPerCar];
GO
IF OBJECT_ID(N'[dbo].[OrigenRental]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrigenRental];
GO
IF OBJECT_ID(N'[dbo].[OrigenVuelo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrigenVuelo];
GO
IF OBJECT_ID(N'[dbo].[Paquete]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paquete];
GO
IF OBJECT_ID(N'[dbo].[Rango]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rango];
GO
IF OBJECT_ID(N'[dbo].[Rangos_Tarifa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rangos_Tarifa];
GO
IF OBJECT_ID(N'[dbo].[Reduccion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reduccion];
GO
IF OBJECT_ID(N'[dbo].[Reduccion_Hotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reduccion_Hotel];
GO
IF OBJECT_ID(N'[dbo].[Rental]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rental];
GO
IF OBJECT_ID(N'[dbo].[Subdestino]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subdestino];
GO
IF OBJECT_ID(N'[dbo].[Suplemento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suplemento];
GO
IF OBJECT_ID(N'[dbo].[Suplemento_Hotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suplemento_Hotel];
GO
IF OBJECT_ID(N'[dbo].[TarifaCarro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TarifaCarro];
GO
IF OBJECT_ID(N'[dbo].[Tarificacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tarificacion];
GO
IF OBJECT_ID(N'[dbo].[Temporada]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Temporada];
GO
IF OBJECT_ID(N'[dbo].[Temporada_Hotel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Temporada_Hotel];
GO
IF OBJECT_ID(N'[dbo].[Temporada_Reduccion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Temporada_Reduccion];
GO
IF OBJECT_ID(N'[dbo].[Temporada_Suplemento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Temporada_Suplemento];
GO
IF OBJECT_ID(N'[dbo].[TemporadaAvion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemporadaAvion];
GO
IF OBJECT_ID(N'[dbo].[TemporadaEmpresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemporadaEmpresa];
GO
IF OBJECT_ID(N'[dbo].[TemporadaExcursion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemporadaExcursion];
GO
IF OBJECT_ID(N'[dbo].[TemporadaPaquete]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemporadaPaquete];
GO
IF OBJECT_ID(N'[dbo].[TemporadaRental]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemporadaRental];
GO
IF OBJECT_ID(N'[dbo].[Tipo_Empresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipo_Empresa];
GO
IF OBJECT_ID(N'[dbo].[Tipo_Tarifa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipo_Tarifa];
GO
IF OBJECT_ID(N'[dbo].[TipoAvion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoAvion];
GO
IF OBJECT_ID(N'[dbo].[Tipol_HotelTipol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipol_HotelTipol];
GO
IF OBJECT_ID(N'[dbo].[Tipologia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipologia];
GO
IF OBJECT_ID(N'[dbo].[Tipologia_Carro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipologia_Carro];
GO
IF OBJECT_ID(N'[TripAgencyModelStoreContainer].[Pai]', 'U') IS NOT NULL
    DROP TABLE [TripAgencyModelStoreContainer].[Pai];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Anno'
CREATE TABLE [dbo].[Anno] (
    [IdAnno] int IDENTITY(1,1) NOT NULL,
    [anno1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Avion'
CREATE TABLE [dbo].[Avion] (
    [idVuelo] int IDENTITY(1,1) NOT NULL,
    [numVuelo] nvarchar(max)  NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL,
    [idEmpresa] int  NOT NULL,
    [tsalida] datetime  NOT NULL,
    [tllegada] datetime  NOT NULL,
    [horallegada] nvarchar(max)  NOT NULL,
    [horasalida] nvarchar(max)  NOT NULL,
    [idTipoAvion] int  NULL,
    [idOrigenVuelo] int  NULL,
    [idDestinoVuelo] int  NULL
);
GO

-- Creating table 'Cadena'
CREATE TABLE [dbo].[Cadena] (
    [idCadena] int IDENTITY(1,1) NOT NULL,
    [cadena_] varchar(max)  NOT NULL,
    [logo_cadena] varchar(max)  NOT NULL,
    [descrpcion_cadena] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Carro'
CREATE TABLE [dbo].[Carro] (
    [idCarro] int IDENTITY(1,1) NOT NULL,
    [foto_carro1] nvarchar(max)  NOT NULL,
    [foto_carro2] nvarchar(max)  NOT NULL,
    [foto_carro3] nvarchar(max)  NOT NULL,
    [idtipologia_carro] int  NOT NULL,
    [descripcion_carro] nvarchar(max)  NOT NULL,
    [idempresa] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categoria'
CREATE TABLE [dbo].[Categoria] (
    [idCategoria] int IDENTITY(1,1) NOT NULL,
    [categoria1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategTarifAvion'
CREATE TABLE [dbo].[CategTarifAvion] (
    [idCategTarifAvion] int IDENTITY(1,1) NOT NULL,
    [codigocat] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL,
    [oneway] bit  NOT NULL,
    [roundtrip] bit  NOT NULL
);
GO

-- Creating table 'Clase'
CREATE TABLE [dbo].[Clase] (
    [idClase] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NULL
);
GO

-- Creating table 'Destino'
CREATE TABLE [dbo].[Destino] (
    [idDestino] int IDENTITY(1,1) NOT NULL,
    [destino1] nvarchar(max)  NOT NULL,
    [decripcion_destino] nvarchar(max)  NOT NULL,
    [foto_destino1] nvarchar(max)  NOT NULL,
    [foto_destino2] nvarchar(max)  NOT NULL,
    [idpais] int  NOT NULL
);
GO

-- Creating table 'DestinoRental'
CREATE TABLE [dbo].[DestinoRental] (
    [idDestinoRental] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DestinoVuelo'
CREATE TABLE [dbo].[DestinoVuelo] (
    [idDestinoVuelo] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Empresa'
CREATE TABLE [dbo].[Empresa] (
    [idEmpresa] int IDENTITY(1,1) NOT NULL,
    [nombre_empresa] nvarchar(max)  NOT NULL,
    [foto_empresa] nvarchar(max)  NOT NULL,
    [direccion_empresa] nvarchar(max)  NOT NULL,
    [telefono_empresa] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Empresa_TipoEmpresa'
CREATE TABLE [dbo].[Empresa_TipoEmpresa] (
    [idunionet] int IDENTITY(1,1) NOT NULL,
    [idtipoempresa] int  NULL,
    [idEmpresa] int  NULL,
    [asignado] bit  NULL
);
GO

-- Creating table 'Estacion'
CREATE TABLE [dbo].[Estacion] (
    [idEstacion] int IDENTITY(1,1) NOT NULL,
    [estacion1] nvarchar(max)  NOT NULL,
    [descripcioon_estacion] nvarchar(max)  NULL
);
GO

-- Creating table 'Excursion'
CREATE TABLE [dbo].[Excursion] (
    [idExcursion] int IDENTITY(1,1) NOT NULL,
    [nombre_excursion] nvarchar(max)  NOT NULL,
    [descrpcion_excursion] nvarchar(max)  NOT NULL,
    [idempresa] int  NOT NULL,
    [idSubdetino] int  NULL,
    [foto1] nvarchar(max)  NULL,
    [foto2] nvarchar(max)  NULL
);
GO

-- Creating table 'Hotel'
CREATE TABLE [dbo].[Hotel] (
    [idHotel] int IDENTITY(1,1) NOT NULL,
    [nombre_hotel] nvarchar(max)  NOT NULL,
    [descripcion_hotel] nvarchar(max)  NULL,
    [direccion] nvarchar(max)  NOT NULL,
    [foto_hotel1] nvarchar(max)  NULL,
    [foto_hote2] nvarchar(max)  NULL,
    [foto_hotel3] nvarchar(max)  NULL,
    [idCategoria] int  NOT NULL,
    [idCadena] int  NOT NULL,
    [idSubdetino] int  NOT NULL
);
GO

-- Creating table 'NomAvionClase'
CREATE TABLE [dbo].[NomAvionClase] (
    [idNomAvionClase] int IDENTITY(1,1) NOT NULL,
    [idVuelo] int  NULL,
    [idClase] int  NULL,
    [asignado] bit  NULL
);
GO

-- Creating table 'NomClaseCatTarAvi'
CREATE TABLE [dbo].[NomClaseCatTarAvi] (
    [idNomClaseCatTarAvi] int IDENTITY(1,1) NOT NULL,
    [idClase] int  NULL,
    [idCategTarifAvion] int  NULL,
    [asignado] bit  NULL
);
GO

-- Creating table 'NomTempAvionClase'
CREATE TABLE [dbo].[NomTempAvionClase] (
    [idNomTempAvionClase] int IDENTITY(1,1) NOT NULL,
    [idVuelo] int  NULL,
    [idTemporadaVuelo] int  NULL,
    [tarifa] float  NULL,
    [idClase] int  NULL
);
GO

-- Creating table 'NomTempRedHotel'
CREATE TABLE [dbo].[NomTempRedHotel] (
    [idtemphotred] int IDENTITY(1,1) NOT NULL,
    [idTemporada_Reduccion] int  NOT NULL,
    [idreduccion] int  NOT NULL,
    [tarifa] float  NULL,
    [idtipotarifa] int  NULL
);
GO

-- Creating table 'NomTempSupHotel'
CREATE TABLE [dbo].[NomTempSupHotel] (
    [idtemphotsup] int IDENTITY(1,1) NOT NULL,
    [idSuplementoHotel] int  NOT NULL,
    [idsuplemento] int  NOT NULL,
    [tarifa] float  NULL,
    [idtipotarifa] int  NULL
);
GO

-- Creating table 'NomTempTipHotel'
CREATE TABLE [dbo].[NomTempTipHotel] (
    [idtemphottip] int IDENTITY(1,1) NOT NULL,
    [idTemporadaHotel] int  NOT NULL,
    [idTiologia] int  NOT NULL,
    [tarifa] float  NULL,
    [idtipotarifa] int  NULL,
    [maxadult] int  NULL,
    [maxninno] int  NULL,
    [minadult] int  NULL,
    [minninno] int  NULL
);
GO

-- Creating table 'OficinaRenta'
CREATE TABLE [dbo].[OficinaRenta] (
    [idOficina] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [imagen] nvarchar(max)  NULL,
    [descripcion] nvarchar(max)  NULL,
    [idSubdestino] int  NULL,
    [idEmpresa] int  NULL
);
GO

-- Creating table 'OficPerCar'
CREATE TABLE [dbo].[OficPerCar] (
    [idOficCar] int IDENTITY(1,1) NOT NULL,
    [idCarro] int  NULL,
    [idOficina] int  NULL
);
GO

-- Creating table 'OrigenRental'
CREATE TABLE [dbo].[OrigenRental] (
    [idOrigenRental] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrigenVuelo'
CREATE TABLE [dbo].[OrigenVuelo] (
    [idOrigenVuelo] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Paquete'
CREATE TABLE [dbo].[Paquete] (
    [idPaquete] int IDENTITY(1,1) NOT NULL,
    [nombre_paquete] nvarchar(max)  NOT NULL,
    [descripcion_paquete] nvarchar(max)  NOT NULL,
    [idEmpresa] int  NOT NULL,
    [foto] nvarchar(max)  NULL
);
GO

-- Creating table 'Rango'
CREATE TABLE [dbo].[Rango] (
    [idrango] int IDENTITY(1,1) NOT NULL,
    [rango1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Rangos_Tarifa'
CREATE TABLE [dbo].[Rangos_Tarifa] (
    [idrangtar] int IDENTITY(1,1) NOT NULL,
    [idcondtarifa] int  NULL,
    [idrango] int  NULL,
    [asignado] bit  NULL,
    [tarifa] float  NULL
);
GO

-- Creating table 'Reduccion'
CREATE TABLE [dbo].[Reduccion] (
    [idReduccion] int IDENTITY(1,1) NOT NULL,
    [reduccion1] nvarchar(max)  NOT NULL,
    [descripcion_reduccion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reduccion_Hotel'
CREATE TABLE [dbo].[Reduccion_Hotel] (
    [idReduccionhotel] int IDENTITY(1,1) NOT NULL,
    [idhotel] int  NULL,
    [idReduccion] int  NULL,
    [asignado] bit  NULL,
    [precio] float  NULL,
    [porciento] float  NULL
);
GO

-- Creating table 'Rental'
CREATE TABLE [dbo].[Rental] (
    [idRental] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [foto] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL,
    [idDestinoRental] int  NOT NULL,
    [idOrigenRental] int  NOT NULL,
    [idEmpresa] int  NULL
);
GO

-- Creating table 'Subdestino'
CREATE TABLE [dbo].[Subdestino] (
    [idSubdestino] int IDENTITY(1,1) NOT NULL,
    [subdestino1] nvarchar(max)  NOT NULL,
    [descripcion_subdestino] nvarchar(max)  NOT NULL,
    [foto_sub1] nvarchar(max)  NOT NULL,
    [foto_sub2] nvarchar(max)  NOT NULL,
    [iddestino] int  NOT NULL
);
GO

-- Creating table 'Suplemento'
CREATE TABLE [dbo].[Suplemento] (
    [idSuplemento] int IDENTITY(1,1) NOT NULL,
    [suplemento1] nvarchar(max)  NOT NULL,
    [descripcion_suplemento] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Suplemento_Hotel'
CREATE TABLE [dbo].[Suplemento_Hotel] (
    [idsuplementohotel] int IDENTITY(1,1) NOT NULL,
    [idhotel] int  NULL,
    [idSuplemento] int  NULL,
    [asignado] bit  NULL,
    [precio] float  NULL,
    [porciento] float  NULL
);
GO

-- Creating table 'TarifaCarro'
CREATE TABLE [dbo].[TarifaCarro] (
    [idcondtarifa] int IDENTITY(1,1) NOT NULL,
    [valor] float  NOT NULL,
    [seguro] float  NOT NULL,
    [deposito] float  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL,
    [idAnno] int  NOT NULL,
    [idTemporada] int  NOT NULL,
    [idtipologia_carro] int  NOT NULL,
    [idEmpresa] int  NOT NULL,
    [idTemporadaEmpresa] int  NULL
);
GO

-- Creating table 'Tarificacion'
CREATE TABLE [dbo].[Tarificacion] (
    [idTarificacion] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [pack] bit  NOT NULL,
    [hab] bit  NOT NULL,
    [casa] bit  NOT NULL,
    [descripcion] nvarchar(max)  NULL
);
GO

-- Creating table 'Temporada'
CREATE TABLE [dbo].[Temporada] (
    [idtemporada] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NULL
);
GO

-- Creating table 'Temporada_Hotel'
CREATE TABLE [dbo].[Temporada_Hotel] (
    [idTemporadaHotel] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [descripcion_periodo] nvarchar(max)  NULL,
    [idanno] int  NOT NULL,
    [idhotel] int  NOT NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL,
    [tarifa] float  NULL
);
GO

-- Creating table 'Temporada_Reduccion'
CREATE TABLE [dbo].[Temporada_Reduccion] (
    [idTemporada_Reduccion] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NULL,
    [fin] datetime  NULL,
    [idtemporada] int  NOT NULL,
    [idanno] int  NOT NULL,
    [valor] float  NULL,
    [idhotel] int  NULL,
    [idEstacion] int  NOT NULL
);
GO

-- Creating table 'Temporada_Suplemento'
CREATE TABLE [dbo].[Temporada_Suplemento] (
    [idSuplementoHotel] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [idanno] int  NOT NULL,
    [valor] float  NULL,
    [idhotel] int  NOT NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL
);
GO

-- Creating table 'TemporadaAvion'
CREATE TABLE [dbo].[TemporadaAvion] (
    [idTemporadaVuelo] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [descripcion_periodo] nvarchar(max)  NULL,
    [idanno] int  NOT NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL,
    [idEmpresa] int  NULL
);
GO

-- Creating table 'TemporadaEmpresa'
CREATE TABLE [dbo].[TemporadaEmpresa] (
    [idTemporadaEmpresa] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [descripcion_periodo] nvarchar(max)  NULL,
    [idanno] int  NOT NULL,
    [idEmpresa] int  NOT NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL
);
GO

-- Creating table 'TemporadaExcursion'
CREATE TABLE [dbo].[TemporadaExcursion] (
    [idtemporadaexcursion] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [descripcion_periodo] nvarchar(max)  NULL,
    [idanno] int  NOT NULL,
    [precio_tarifa] float  NULL,
    [idExcursion] int  NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL
);
GO

-- Creating table 'TemporadaPaquete'
CREATE TABLE [dbo].[TemporadaPaquete] (
    [idtemporadapaquete] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [descripcion_periodo] nvarchar(max)  NULL,
    [idanno] int  NOT NULL,
    [precio_tarifa] float  NULL,
    [idPaquete] int  NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL
);
GO

-- Creating table 'TemporadaRental'
CREATE TABLE [dbo].[TemporadaRental] (
    [idTemporadaRental] int IDENTITY(1,1) NOT NULL,
    [periodo] nvarchar(max)  NOT NULL,
    [inicio] datetime  NOT NULL,
    [fin] datetime  NOT NULL,
    [descripcion_periodo] nvarchar(max)  NULL,
    [idanno] int  NOT NULL,
    [precio_tarifa] float  NULL,
    [idRental] int  NULL,
    [idEstacion] int  NOT NULL,
    [idTemporada] int  NOT NULL
);
GO

-- Creating table 'Tipo_Empresa'
CREATE TABLE [dbo].[Tipo_Empresa] (
    [idtipoempresa] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Tipo_Tarifa'
CREATE TABLE [dbo].[Tipo_Tarifa] (
    [idTipoTarifa] int IDENTITY(1,1) NOT NULL,
    [tipo_tarifa1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TipoAvion'
CREATE TABLE [dbo].[TipoAvion] (
    [idTipoAvion] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NULL
);
GO

-- Creating table 'Tipol_HotelTipol'
CREATE TABLE [dbo].[Tipol_HotelTipol] (
    [idtipologiahotel] int IDENTITY(1,1) NOT NULL,
    [idhotel] int  NULL,
    [idTiologia] int  NULL,
    [asignado] bit  NULL
);
GO

-- Creating table 'Tipologia'
CREATE TABLE [dbo].[Tipologia] (
    [idTiologia] int IDENTITY(1,1) NOT NULL,
    [tipologia1] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NULL,
    [idTarificacion] int  NULL
);
GO

-- Creating table 'Tipologia_Carro'
CREATE TABLE [dbo].[Tipologia_Carro] (
    [idtipologia_carro] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Pai'
CREATE TABLE [dbo].[Pai] (
    [idPais] int IDENTITY(1,1) NOT NULL,
    [pais] nvarchar(max)  NOT NULL,
    [foto] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAnno] in table 'Anno'
ALTER TABLE [dbo].[Anno]
ADD CONSTRAINT [PK_Anno]
    PRIMARY KEY CLUSTERED ([IdAnno] ASC);
GO

-- Creating primary key on [idVuelo] in table 'Avion'
ALTER TABLE [dbo].[Avion]
ADD CONSTRAINT [PK_Avion]
    PRIMARY KEY CLUSTERED ([idVuelo] ASC);
GO

-- Creating primary key on [idCadena] in table 'Cadena'
ALTER TABLE [dbo].[Cadena]
ADD CONSTRAINT [PK_Cadena]
    PRIMARY KEY CLUSTERED ([idCadena] ASC);
GO

-- Creating primary key on [idCarro] in table 'Carro'
ALTER TABLE [dbo].[Carro]
ADD CONSTRAINT [PK_Carro]
    PRIMARY KEY CLUSTERED ([idCarro] ASC);
GO

-- Creating primary key on [idCategoria] in table 'Categoria'
ALTER TABLE [dbo].[Categoria]
ADD CONSTRAINT [PK_Categoria]
    PRIMARY KEY CLUSTERED ([idCategoria] ASC);
GO

-- Creating primary key on [idCategTarifAvion] in table 'CategTarifAvion'
ALTER TABLE [dbo].[CategTarifAvion]
ADD CONSTRAINT [PK_CategTarifAvion]
    PRIMARY KEY CLUSTERED ([idCategTarifAvion] ASC);
GO

-- Creating primary key on [idClase] in table 'Clase'
ALTER TABLE [dbo].[Clase]
ADD CONSTRAINT [PK_Clase]
    PRIMARY KEY CLUSTERED ([idClase] ASC);
GO

-- Creating primary key on [idDestino] in table 'Destino'
ALTER TABLE [dbo].[Destino]
ADD CONSTRAINT [PK_Destino]
    PRIMARY KEY CLUSTERED ([idDestino] ASC);
GO

-- Creating primary key on [idDestinoRental] in table 'DestinoRental'
ALTER TABLE [dbo].[DestinoRental]
ADD CONSTRAINT [PK_DestinoRental]
    PRIMARY KEY CLUSTERED ([idDestinoRental] ASC);
GO

-- Creating primary key on [idDestinoVuelo] in table 'DestinoVuelo'
ALTER TABLE [dbo].[DestinoVuelo]
ADD CONSTRAINT [PK_DestinoVuelo]
    PRIMARY KEY CLUSTERED ([idDestinoVuelo] ASC);
GO

-- Creating primary key on [idEmpresa] in table 'Empresa'
ALTER TABLE [dbo].[Empresa]
ADD CONSTRAINT [PK_Empresa]
    PRIMARY KEY CLUSTERED ([idEmpresa] ASC);
GO

-- Creating primary key on [idunionet] in table 'Empresa_TipoEmpresa'
ALTER TABLE [dbo].[Empresa_TipoEmpresa]
ADD CONSTRAINT [PK_Empresa_TipoEmpresa]
    PRIMARY KEY CLUSTERED ([idunionet] ASC);
GO

-- Creating primary key on [idEstacion] in table 'Estacion'
ALTER TABLE [dbo].[Estacion]
ADD CONSTRAINT [PK_Estacion]
    PRIMARY KEY CLUSTERED ([idEstacion] ASC);
GO

-- Creating primary key on [idExcursion] in table 'Excursion'
ALTER TABLE [dbo].[Excursion]
ADD CONSTRAINT [PK_Excursion]
    PRIMARY KEY CLUSTERED ([idExcursion] ASC);
GO

-- Creating primary key on [idHotel] in table 'Hotel'
ALTER TABLE [dbo].[Hotel]
ADD CONSTRAINT [PK_Hotel]
    PRIMARY KEY CLUSTERED ([idHotel] ASC);
GO

-- Creating primary key on [idNomAvionClase] in table 'NomAvionClase'
ALTER TABLE [dbo].[NomAvionClase]
ADD CONSTRAINT [PK_NomAvionClase]
    PRIMARY KEY CLUSTERED ([idNomAvionClase] ASC);
GO

-- Creating primary key on [idNomClaseCatTarAvi] in table 'NomClaseCatTarAvi'
ALTER TABLE [dbo].[NomClaseCatTarAvi]
ADD CONSTRAINT [PK_NomClaseCatTarAvi]
    PRIMARY KEY CLUSTERED ([idNomClaseCatTarAvi] ASC);
GO

-- Creating primary key on [idNomTempAvionClase] in table 'NomTempAvionClase'
ALTER TABLE [dbo].[NomTempAvionClase]
ADD CONSTRAINT [PK_NomTempAvionClase]
    PRIMARY KEY CLUSTERED ([idNomTempAvionClase] ASC);
GO

-- Creating primary key on [idtemphotred] in table 'NomTempRedHotel'
ALTER TABLE [dbo].[NomTempRedHotel]
ADD CONSTRAINT [PK_NomTempRedHotel]
    PRIMARY KEY CLUSTERED ([idtemphotred] ASC);
GO

-- Creating primary key on [idtemphotsup] in table 'NomTempSupHotel'
ALTER TABLE [dbo].[NomTempSupHotel]
ADD CONSTRAINT [PK_NomTempSupHotel]
    PRIMARY KEY CLUSTERED ([idtemphotsup] ASC);
GO

-- Creating primary key on [idtemphottip] in table 'NomTempTipHotel'
ALTER TABLE [dbo].[NomTempTipHotel]
ADD CONSTRAINT [PK_NomTempTipHotel]
    PRIMARY KEY CLUSTERED ([idtemphottip] ASC);
GO

-- Creating primary key on [idOficina] in table 'OficinaRenta'
ALTER TABLE [dbo].[OficinaRenta]
ADD CONSTRAINT [PK_OficinaRenta]
    PRIMARY KEY CLUSTERED ([idOficina] ASC);
GO

-- Creating primary key on [idOficCar] in table 'OficPerCar'
ALTER TABLE [dbo].[OficPerCar]
ADD CONSTRAINT [PK_OficPerCar]
    PRIMARY KEY CLUSTERED ([idOficCar] ASC);
GO

-- Creating primary key on [idOrigenRental] in table 'OrigenRental'
ALTER TABLE [dbo].[OrigenRental]
ADD CONSTRAINT [PK_OrigenRental]
    PRIMARY KEY CLUSTERED ([idOrigenRental] ASC);
GO

-- Creating primary key on [idOrigenVuelo] in table 'OrigenVuelo'
ALTER TABLE [dbo].[OrigenVuelo]
ADD CONSTRAINT [PK_OrigenVuelo]
    PRIMARY KEY CLUSTERED ([idOrigenVuelo] ASC);
GO

-- Creating primary key on [idPaquete] in table 'Paquete'
ALTER TABLE [dbo].[Paquete]
ADD CONSTRAINT [PK_Paquete]
    PRIMARY KEY CLUSTERED ([idPaquete] ASC);
GO

-- Creating primary key on [idrango] in table 'Rango'
ALTER TABLE [dbo].[Rango]
ADD CONSTRAINT [PK_Rango]
    PRIMARY KEY CLUSTERED ([idrango] ASC);
GO

-- Creating primary key on [idrangtar] in table 'Rangos_Tarifa'
ALTER TABLE [dbo].[Rangos_Tarifa]
ADD CONSTRAINT [PK_Rangos_Tarifa]
    PRIMARY KEY CLUSTERED ([idrangtar] ASC);
GO

-- Creating primary key on [idReduccion] in table 'Reduccion'
ALTER TABLE [dbo].[Reduccion]
ADD CONSTRAINT [PK_Reduccion]
    PRIMARY KEY CLUSTERED ([idReduccion] ASC);
GO

-- Creating primary key on [idReduccionhotel] in table 'Reduccion_Hotel'
ALTER TABLE [dbo].[Reduccion_Hotel]
ADD CONSTRAINT [PK_Reduccion_Hotel]
    PRIMARY KEY CLUSTERED ([idReduccionhotel] ASC);
GO

-- Creating primary key on [idRental] in table 'Rental'
ALTER TABLE [dbo].[Rental]
ADD CONSTRAINT [PK_Rental]
    PRIMARY KEY CLUSTERED ([idRental] ASC);
GO

-- Creating primary key on [idSubdestino] in table 'Subdestino'
ALTER TABLE [dbo].[Subdestino]
ADD CONSTRAINT [PK_Subdestino]
    PRIMARY KEY CLUSTERED ([idSubdestino] ASC);
GO

-- Creating primary key on [idSuplemento] in table 'Suplemento'
ALTER TABLE [dbo].[Suplemento]
ADD CONSTRAINT [PK_Suplemento]
    PRIMARY KEY CLUSTERED ([idSuplemento] ASC);
GO

-- Creating primary key on [idsuplementohotel] in table 'Suplemento_Hotel'
ALTER TABLE [dbo].[Suplemento_Hotel]
ADD CONSTRAINT [PK_Suplemento_Hotel]
    PRIMARY KEY CLUSTERED ([idsuplementohotel] ASC);
GO

-- Creating primary key on [idcondtarifa] in table 'TarifaCarro'
ALTER TABLE [dbo].[TarifaCarro]
ADD CONSTRAINT [PK_TarifaCarro]
    PRIMARY KEY CLUSTERED ([idcondtarifa] ASC);
GO

-- Creating primary key on [idTarificacion] in table 'Tarificacion'
ALTER TABLE [dbo].[Tarificacion]
ADD CONSTRAINT [PK_Tarificacion]
    PRIMARY KEY CLUSTERED ([idTarificacion] ASC);
GO

-- Creating primary key on [idtemporada] in table 'Temporada'
ALTER TABLE [dbo].[Temporada]
ADD CONSTRAINT [PK_Temporada]
    PRIMARY KEY CLUSTERED ([idtemporada] ASC);
GO

-- Creating primary key on [idTemporadaHotel] in table 'Temporada_Hotel'
ALTER TABLE [dbo].[Temporada_Hotel]
ADD CONSTRAINT [PK_Temporada_Hotel]
    PRIMARY KEY CLUSTERED ([idTemporadaHotel] ASC);
GO

-- Creating primary key on [idTemporada_Reduccion] in table 'Temporada_Reduccion'
ALTER TABLE [dbo].[Temporada_Reduccion]
ADD CONSTRAINT [PK_Temporada_Reduccion]
    PRIMARY KEY CLUSTERED ([idTemporada_Reduccion] ASC);
GO

-- Creating primary key on [idSuplementoHotel] in table 'Temporada_Suplemento'
ALTER TABLE [dbo].[Temporada_Suplemento]
ADD CONSTRAINT [PK_Temporada_Suplemento]
    PRIMARY KEY CLUSTERED ([idSuplementoHotel] ASC);
GO

-- Creating primary key on [idTemporadaVuelo] in table 'TemporadaAvion'
ALTER TABLE [dbo].[TemporadaAvion]
ADD CONSTRAINT [PK_TemporadaAvion]
    PRIMARY KEY CLUSTERED ([idTemporadaVuelo] ASC);
GO

-- Creating primary key on [idTemporadaEmpresa] in table 'TemporadaEmpresa'
ALTER TABLE [dbo].[TemporadaEmpresa]
ADD CONSTRAINT [PK_TemporadaEmpresa]
    PRIMARY KEY CLUSTERED ([idTemporadaEmpresa] ASC);
GO

-- Creating primary key on [idtemporadaexcursion] in table 'TemporadaExcursion'
ALTER TABLE [dbo].[TemporadaExcursion]
ADD CONSTRAINT [PK_TemporadaExcursion]
    PRIMARY KEY CLUSTERED ([idtemporadaexcursion] ASC);
GO

-- Creating primary key on [idtemporadapaquete] in table 'TemporadaPaquete'
ALTER TABLE [dbo].[TemporadaPaquete]
ADD CONSTRAINT [PK_TemporadaPaquete]
    PRIMARY KEY CLUSTERED ([idtemporadapaquete] ASC);
GO

-- Creating primary key on [idTemporadaRental] in table 'TemporadaRental'
ALTER TABLE [dbo].[TemporadaRental]
ADD CONSTRAINT [PK_TemporadaRental]
    PRIMARY KEY CLUSTERED ([idTemporadaRental] ASC);
GO

-- Creating primary key on [idtipoempresa] in table 'Tipo_Empresa'
ALTER TABLE [dbo].[Tipo_Empresa]
ADD CONSTRAINT [PK_Tipo_Empresa]
    PRIMARY KEY CLUSTERED ([idtipoempresa] ASC);
GO

-- Creating primary key on [idTipoTarifa] in table 'Tipo_Tarifa'
ALTER TABLE [dbo].[Tipo_Tarifa]
ADD CONSTRAINT [PK_Tipo_Tarifa]
    PRIMARY KEY CLUSTERED ([idTipoTarifa] ASC);
GO

-- Creating primary key on [idTipoAvion] in table 'TipoAvion'
ALTER TABLE [dbo].[TipoAvion]
ADD CONSTRAINT [PK_TipoAvion]
    PRIMARY KEY CLUSTERED ([idTipoAvion] ASC);
GO

-- Creating primary key on [idtipologiahotel] in table 'Tipol_HotelTipol'
ALTER TABLE [dbo].[Tipol_HotelTipol]
ADD CONSTRAINT [PK_Tipol_HotelTipol]
    PRIMARY KEY CLUSTERED ([idtipologiahotel] ASC);
GO

-- Creating primary key on [idTiologia] in table 'Tipologia'
ALTER TABLE [dbo].[Tipologia]
ADD CONSTRAINT [PK_Tipologia]
    PRIMARY KEY CLUSTERED ([idTiologia] ASC);
GO

-- Creating primary key on [idtipologia_carro] in table 'Tipologia_Carro'
ALTER TABLE [dbo].[Tipologia_Carro]
ADD CONSTRAINT [PK_Tipologia_Carro]
    PRIMARY KEY CLUSTERED ([idtipologia_carro] ASC);
GO

-- Creating primary key on [idPais], [pais] in table 'Pai'
ALTER TABLE [dbo].[Pai]
ADD CONSTRAINT [PK_Pai]
    PRIMARY KEY CLUSTERED ([idPais], [pais] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idAnno] in table 'TarifaCarro'
ALTER TABLE [dbo].[TarifaCarro]
ADD CONSTRAINT [FK_TarifaCarro_ToTable_1]
    FOREIGN KEY ([idAnno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TarifaCarro_ToTable_1'
CREATE INDEX [IX_FK_TarifaCarro_ToTable_1]
ON [dbo].[TarifaCarro]
    ([idAnno]);
GO

-- Creating foreign key on [idanno] in table 'TemporadaAvion'
ALTER TABLE [dbo].[TemporadaAvion]
ADD CONSTRAINT [FK_Temporada_Avion_ToTable_3]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Avion_ToTable_3'
CREATE INDEX [IX_FK_Temporada_Avion_ToTable_3]
ON [dbo].[TemporadaAvion]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'TemporadaExcursion'
ALTER TABLE [dbo].[TemporadaExcursion]
ADD CONSTRAINT [FK_Temporada_Excursion_ToTable_1]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Excursion_ToTable_1'
CREATE INDEX [IX_FK_Temporada_Excursion_ToTable_1]
ON [dbo].[TemporadaExcursion]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'Temporada_Hotel'
ALTER TABLE [dbo].[Temporada_Hotel]
ADD CONSTRAINT [FK_Temporada_Hotel_ToTable_3]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Hotel_ToTable_3'
CREATE INDEX [IX_FK_Temporada_Hotel_ToTable_3]
ON [dbo].[Temporada_Hotel]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'TemporadaPaquete'
ALTER TABLE [dbo].[TemporadaPaquete]
ADD CONSTRAINT [FK_Temporada_Paquete_ToTable]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Paquete_ToTable'
CREATE INDEX [IX_FK_Temporada_Paquete_ToTable]
ON [dbo].[TemporadaPaquete]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'Temporada_Reduccion'
ALTER TABLE [dbo].[Temporada_Reduccion]
ADD CONSTRAINT [FK_Temporada_Reduccion_ToTable_6]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Reduccion_ToTable_6'
CREATE INDEX [IX_FK_Temporada_Reduccion_ToTable_6]
ON [dbo].[Temporada_Reduccion]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'Temporada_Suplemento'
ALTER TABLE [dbo].[Temporada_Suplemento]
ADD CONSTRAINT [FK_Temporada_Suplemento_ToTable]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Suplemento_ToTable'
CREATE INDEX [IX_FK_Temporada_Suplemento_ToTable]
ON [dbo].[Temporada_Suplemento]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'TemporadaEmpresa'
ALTER TABLE [dbo].[TemporadaEmpresa]
ADD CONSTRAINT [FK_TemporadaEmpresa_ToTable_3]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaEmpresa_ToTable_3'
CREATE INDEX [IX_FK_TemporadaEmpresa_ToTable_3]
ON [dbo].[TemporadaEmpresa]
    ([idanno]);
GO

-- Creating foreign key on [idanno] in table 'TemporadaRental'
ALTER TABLE [dbo].[TemporadaRental]
ADD CONSTRAINT [FK_TemporadaRental_ToTable_1]
    FOREIGN KEY ([idanno])
    REFERENCES [dbo].[Anno]
        ([IdAnno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaRental_ToTable_1'
CREATE INDEX [IX_FK_TemporadaRental_ToTable_1]
ON [dbo].[TemporadaRental]
    ([idanno]);
GO

-- Creating foreign key on [idOrigenVuelo] in table 'Avion'
ALTER TABLE [dbo].[Avion]
ADD CONSTRAINT [FK_Avion_ToTable]
    FOREIGN KEY ([idOrigenVuelo])
    REFERENCES [dbo].[OrigenVuelo]
        ([idOrigenVuelo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Avion_ToTable'
CREATE INDEX [IX_FK_Avion_ToTable]
ON [dbo].[Avion]
    ([idOrigenVuelo]);
GO

-- Creating foreign key on [idDestinoVuelo] in table 'Avion'
ALTER TABLE [dbo].[Avion]
ADD CONSTRAINT [FK_Avion_ToTable_1]
    FOREIGN KEY ([idDestinoVuelo])
    REFERENCES [dbo].[DestinoVuelo]
        ([idDestinoVuelo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Avion_ToTable_1'
CREATE INDEX [IX_FK_Avion_ToTable_1]
ON [dbo].[Avion]
    ([idDestinoVuelo]);
GO

-- Creating foreign key on [idVuelo] in table 'NomAvionClase'
ALTER TABLE [dbo].[NomAvionClase]
ADD CONSTRAINT [FK_NomAvionClase_ToTable]
    FOREIGN KEY ([idVuelo])
    REFERENCES [dbo].[Avion]
        ([idVuelo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NomAvionClase_ToTable'
CREATE INDEX [IX_FK_NomAvionClase_ToTable]
ON [dbo].[NomAvionClase]
    ([idVuelo]);
GO

-- Creating foreign key on [idVuelo] in table 'NomTempAvionClase'
ALTER TABLE [dbo].[NomTempAvionClase]
ADD CONSTRAINT [FK_NomTempClase_ToTable]
    FOREIGN KEY ([idVuelo])
    REFERENCES [dbo].[Avion]
        ([idVuelo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NomTempClase_ToTable'
CREATE INDEX [IX_FK_NomTempClase_ToTable]
ON [dbo].[NomTempAvionClase]
    ([idVuelo]);
GO

-- Creating foreign key on [idEmpresa] in table 'Avion'
ALTER TABLE [dbo].[Avion]
ADD CONSTRAINT [FK_Vuelo_ToTable2]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Vuelo_ToTable2'
CREATE INDEX [IX_FK_Vuelo_ToTable2]
ON [dbo].[Avion]
    ([idEmpresa]);
GO

-- Creating foreign key on [idTipoAvion] in table 'Avion'
ALTER TABLE [dbo].[Avion]
ADD CONSTRAINT [FK_Vuelo_ToTable4]
    FOREIGN KEY ([idTipoAvion])
    REFERENCES [dbo].[TipoAvion]
        ([idTipoAvion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Vuelo_ToTable4'
CREATE INDEX [IX_FK_Vuelo_ToTable4]
ON [dbo].[Avion]
    ([idTipoAvion]);
GO

-- Creating foreign key on [idCadena] in table 'Hotel'
ALTER TABLE [dbo].[Hotel]
ADD CONSTRAINT [FK_Hotel_ToTable_1]
    FOREIGN KEY ([idCadena])
    REFERENCES [dbo].[Cadena]
        ([idCadena])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Hotel_ToTable_1'
CREATE INDEX [IX_FK_Hotel_ToTable_1]
ON [dbo].[Hotel]
    ([idCadena]);
GO

-- Creating foreign key on [idempresa] in table 'Carro'
ALTER TABLE [dbo].[Carro]
ADD CONSTRAINT [FK_Carros_ToTable]
    FOREIGN KEY ([idempresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Carros_ToTable'
CREATE INDEX [IX_FK_Carros_ToTable]
ON [dbo].[Carro]
    ([idempresa]);
GO

-- Creating foreign key on [idtipologia_carro] in table 'Carro'
ALTER TABLE [dbo].[Carro]
ADD CONSTRAINT [FK_Carros_ToTable_2]
    FOREIGN KEY ([idtipologia_carro])
    REFERENCES [dbo].[Tipologia_Carro]
        ([idtipologia_carro])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Carros_ToTable_2'
CREATE INDEX [IX_FK_Carros_ToTable_2]
ON [dbo].[Carro]
    ([idtipologia_carro]);
GO

-- Creating foreign key on [idCarro] in table 'OficPerCar'
ALTER TABLE [dbo].[OficPerCar]
ADD CONSTRAINT [FK_OficPerCar_ToTable]
    FOREIGN KEY ([idCarro])
    REFERENCES [dbo].[Carro]
        ([idCarro])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OficPerCar_ToTable'
CREATE INDEX [IX_FK_OficPerCar_ToTable]
ON [dbo].[OficPerCar]
    ([idCarro]);
GO

-- Creating foreign key on [idCategoria] in table 'Hotel'
ALTER TABLE [dbo].[Hotel]
ADD CONSTRAINT [FK_Hotel_ToTable]
    FOREIGN KEY ([idCategoria])
    REFERENCES [dbo].[Categoria]
        ([idCategoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Hotel_ToTable'
CREATE INDEX [IX_FK_Hotel_ToTable]
ON [dbo].[Hotel]
    ([idCategoria]);
GO

-- Creating foreign key on [idCategTarifAvion] in table 'NomClaseCatTarAvi'
ALTER TABLE [dbo].[NomClaseCatTarAvi]
ADD CONSTRAINT [FK_ClaseCatTarAvi_ToTable]
    FOREIGN KEY ([idCategTarifAvion])
    REFERENCES [dbo].[CategTarifAvion]
        ([idCategTarifAvion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClaseCatTarAvi_ToTable'
CREATE INDEX [IX_FK_ClaseCatTarAvi_ToTable]
ON [dbo].[NomClaseCatTarAvi]
    ([idCategTarifAvion]);
GO

-- Creating foreign key on [idClase] in table 'NomClaseCatTarAvi'
ALTER TABLE [dbo].[NomClaseCatTarAvi]
ADD CONSTRAINT [FK_ClaseCatTarAvi_ToTable_1]
    FOREIGN KEY ([idClase])
    REFERENCES [dbo].[Clase]
        ([idClase])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClaseCatTarAvi_ToTable_1'
CREATE INDEX [IX_FK_ClaseCatTarAvi_ToTable_1]
ON [dbo].[NomClaseCatTarAvi]
    ([idClase]);
GO

-- Creating foreign key on [idClase] in table 'NomAvionClase'
ALTER TABLE [dbo].[NomAvionClase]
ADD CONSTRAINT [FK_NomAvionClase_ToTable_1]
    FOREIGN KEY ([idClase])
    REFERENCES [dbo].[Clase]
        ([idClase])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NomAvionClase_ToTable_1'
CREATE INDEX [IX_FK_NomAvionClase_ToTable_1]
ON [dbo].[NomAvionClase]
    ([idClase]);
GO

-- Creating foreign key on [idClase] in table 'NomTempAvionClase'
ALTER TABLE [dbo].[NomTempAvionClase]
ADD CONSTRAINT [FK_NomTempClase_ToTable_2]
    FOREIGN KEY ([idClase])
    REFERENCES [dbo].[Clase]
        ([idClase])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NomTempClase_ToTable_2'
CREATE INDEX [IX_FK_NomTempClase_ToTable_2]
ON [dbo].[NomTempAvionClase]
    ([idClase]);
GO

-- Creating foreign key on [iddestino] in table 'Subdestino'
ALTER TABLE [dbo].[Subdestino]
ADD CONSTRAINT [FK_Subdestino_ToTable]
    FOREIGN KEY ([iddestino])
    REFERENCES [dbo].[Destino]
        ([idDestino])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Subdestino_ToTable'
CREATE INDEX [IX_FK_Subdestino_ToTable]
ON [dbo].[Subdestino]
    ([iddestino]);
GO

-- Creating foreign key on [idDestinoRental] in table 'Rental'
ALTER TABLE [dbo].[Rental]
ADD CONSTRAINT [FK_Rental_ToTable]
    FOREIGN KEY ([idDestinoRental])
    REFERENCES [dbo].[DestinoRental]
        ([idDestinoRental])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Rental_ToTable'
CREATE INDEX [IX_FK_Rental_ToTable]
ON [dbo].[Rental]
    ([idDestinoRental]);
GO

-- Creating foreign key on [idempresa] in table 'Excursion'
ALTER TABLE [dbo].[Excursion]
ADD CONSTRAINT [FK_Excursion_ToTable]
    FOREIGN KEY ([idempresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Excursion_ToTable'
CREATE INDEX [IX_FK_Excursion_ToTable]
ON [dbo].[Excursion]
    ([idempresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'OficinaRenta'
ALTER TABLE [dbo].[OficinaRenta]
ADD CONSTRAINT [FK_OficinaRenta_ToTable_1]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OficinaRenta_ToTable_1'
CREATE INDEX [IX_FK_OficinaRenta_ToTable_1]
ON [dbo].[OficinaRenta]
    ([idEmpresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'Paquete'
ALTER TABLE [dbo].[Paquete]
ADD CONSTRAINT [FK_Paquete_ToTable]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Paquete_ToTable'
CREATE INDEX [IX_FK_Paquete_ToTable]
ON [dbo].[Paquete]
    ([idEmpresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'Rental'
ALTER TABLE [dbo].[Rental]
ADD CONSTRAINT [FK_Rental_ToTable_2]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Rental_ToTable_2'
CREATE INDEX [IX_FK_Rental_ToTable_2]
ON [dbo].[Rental]
    ([idEmpresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'Empresa_TipoEmpresa'
ALTER TABLE [dbo].[Empresa_TipoEmpresa]
ADD CONSTRAINT [FK_Table_ToTable_1]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Table_ToTable_1'
CREATE INDEX [IX_FK_Table_ToTable_1]
ON [dbo].[Empresa_TipoEmpresa]
    ([idEmpresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'TarifaCarro'
ALTER TABLE [dbo].[TarifaCarro]
ADD CONSTRAINT [FK_TarifaCarro_ToTable_3]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TarifaCarro_ToTable_3'
CREATE INDEX [IX_FK_TarifaCarro_ToTable_3]
ON [dbo].[TarifaCarro]
    ([idEmpresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'TemporadaAvion'
ALTER TABLE [dbo].[TemporadaAvion]
ADD CONSTRAINT [FK_Temporada_Avion_ToTable_2]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Avion_ToTable_2'
CREATE INDEX [IX_FK_Temporada_Avion_ToTable_2]
ON [dbo].[TemporadaAvion]
    ([idEmpresa]);
GO

-- Creating foreign key on [idEmpresa] in table 'TemporadaEmpresa'
ALTER TABLE [dbo].[TemporadaEmpresa]
ADD CONSTRAINT [FK_TemporadaEmpresa_ToTable_7]
    FOREIGN KEY ([idEmpresa])
    REFERENCES [dbo].[Empresa]
        ([idEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaEmpresa_ToTable_7'
CREATE INDEX [IX_FK_TemporadaEmpresa_ToTable_7]
ON [dbo].[TemporadaEmpresa]
    ([idEmpresa]);
GO

-- Creating foreign key on [idtipoempresa] in table 'Empresa_TipoEmpresa'
ALTER TABLE [dbo].[Empresa_TipoEmpresa]
ADD CONSTRAINT [FK_Table_ToTable]
    FOREIGN KEY ([idtipoempresa])
    REFERENCES [dbo].[Tipo_Empresa]
        ([idtipoempresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Table_ToTable'
CREATE INDEX [IX_FK_Table_ToTable]
ON [dbo].[Empresa_TipoEmpresa]
    ([idtipoempresa]);
GO

-- Creating foreign key on [idEstacion] in table 'TemporadaAvion'
ALTER TABLE [dbo].[TemporadaAvion]
ADD CONSTRAINT [FK_Temporada_Avion_ToTable_4]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Avion_ToTable_4'
CREATE INDEX [IX_FK_Temporada_Avion_ToTable_4]
ON [dbo].[TemporadaAvion]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'TemporadaExcursion'
ALTER TABLE [dbo].[TemporadaExcursion]
ADD CONSTRAINT [FK_Temporada_Excursion_ToTable_2]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Excursion_ToTable_2'
CREATE INDEX [IX_FK_Temporada_Excursion_ToTable_2]
ON [dbo].[TemporadaExcursion]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'Temporada_Hotel'
ALTER TABLE [dbo].[Temporada_Hotel]
ADD CONSTRAINT [FK_Temporada_Hotel_ToTable_4]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Hotel_ToTable_4'
CREATE INDEX [IX_FK_Temporada_Hotel_ToTable_4]
ON [dbo].[Temporada_Hotel]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'TemporadaPaquete'
ALTER TABLE [dbo].[TemporadaPaquete]
ADD CONSTRAINT [FK_Temporada_Paquete_ToTable_2]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Paquete_ToTable_2'
CREATE INDEX [IX_FK_Temporada_Paquete_ToTable_2]
ON [dbo].[TemporadaPaquete]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'Temporada_Reduccion'
ALTER TABLE [dbo].[Temporada_Reduccion]
ADD CONSTRAINT [FK_Temporada_Reduccion_ToTable_3]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Reduccion_ToTable_3'
CREATE INDEX [IX_FK_Temporada_Reduccion_ToTable_3]
ON [dbo].[Temporada_Reduccion]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'Temporada_Suplemento'
ALTER TABLE [dbo].[Temporada_Suplemento]
ADD CONSTRAINT [FK_Temporada_Suplemento_ToTable_4]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Suplemento_ToTable_4'
CREATE INDEX [IX_FK_Temporada_Suplemento_ToTable_4]
ON [dbo].[Temporada_Suplemento]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'TemporadaEmpresa'
ALTER TABLE [dbo].[TemporadaEmpresa]
ADD CONSTRAINT [FK_TemporadaEmpresa_ToTable_4]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaEmpresa_ToTable_4'
CREATE INDEX [IX_FK_TemporadaEmpresa_ToTable_4]
ON [dbo].[TemporadaEmpresa]
    ([idEstacion]);
GO

-- Creating foreign key on [idEstacion] in table 'TemporadaRental'
ALTER TABLE [dbo].[TemporadaRental]
ADD CONSTRAINT [FK_TemporadaRental_ToTable_2]
    FOREIGN KEY ([idEstacion])
    REFERENCES [dbo].[Estacion]
        ([idEstacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaRental_ToTable_2'
CREATE INDEX [IX_FK_TemporadaRental_ToTable_2]
ON [dbo].[TemporadaRental]
    ([idEstacion]);
GO

-- Creating foreign key on [idSubdetino] in table 'Excursion'
ALTER TABLE [dbo].[Excursion]
ADD CONSTRAINT [FK_Excursion_ToTable2]
    FOREIGN KEY ([idSubdetino])
    REFERENCES [dbo].[Subdestino]
        ([idSubdestino])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Excursion_ToTable2'
CREATE INDEX [IX_FK_Excursion_ToTable2]
ON [dbo].[Excursion]
    ([idSubdetino]);
GO

-- Creating foreign key on [idExcursion] in table 'TemporadaExcursion'
ALTER TABLE [dbo].[TemporadaExcursion]
ADD CONSTRAINT [FK_Temporada_Excursion_ToTable]
    FOREIGN KEY ([idExcursion])
    REFERENCES [dbo].[Excursion]
        ([idExcursion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Excursion_ToTable'
CREATE INDEX [IX_FK_Temporada_Excursion_ToTable]
ON [dbo].[TemporadaExcursion]
    ([idExcursion]);
GO

-- Creating foreign key on [idSubdetino] in table 'Hotel'
ALTER TABLE [dbo].[Hotel]
ADD CONSTRAINT [FK_Hotel_ToTable_2]
    FOREIGN KEY ([idSubdetino])
    REFERENCES [dbo].[Subdestino]
        ([idSubdestino])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Hotel_ToTable_2'
CREATE INDEX [IX_FK_Hotel_ToTable_2]
ON [dbo].[Hotel]
    ([idSubdetino]);
GO

-- Creating foreign key on [idhotel] in table 'Temporada_Hotel'
ALTER TABLE [dbo].[Temporada_Hotel]
ADD CONSTRAINT [FK_Temporada_Hotel_ToTable]
    FOREIGN KEY ([idhotel])
    REFERENCES [dbo].[Hotel]
        ([idHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Hotel_ToTable'
CREATE INDEX [IX_FK_Temporada_Hotel_ToTable]
ON [dbo].[Temporada_Hotel]
    ([idhotel]);
GO

-- Creating foreign key on [idhotel] in table 'Temporada_Reduccion'
ALTER TABLE [dbo].[Temporada_Reduccion]
ADD CONSTRAINT [FK_Temporada_Reduccion_ToTable]
    FOREIGN KEY ([idhotel])
    REFERENCES [dbo].[Hotel]
        ([idHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Reduccion_ToTable'
CREATE INDEX [IX_FK_Temporada_Reduccion_ToTable]
ON [dbo].[Temporada_Reduccion]
    ([idhotel]);
GO

-- Creating foreign key on [idhotel] in table 'Temporada_Suplemento'
ALTER TABLE [dbo].[Temporada_Suplemento]
ADD CONSTRAINT [FK_Temporada_Suplemento_ToTable_1]
    FOREIGN KEY ([idhotel])
    REFERENCES [dbo].[Hotel]
        ([idHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Suplemento_ToTable_1'
CREATE INDEX [IX_FK_Temporada_Suplemento_ToTable_1]
ON [dbo].[Temporada_Suplemento]
    ([idhotel]);
GO

-- Creating foreign key on [idhotel] in table 'Tipol_HotelTipol'
ALTER TABLE [dbo].[Tipol_HotelTipol]
ADD CONSTRAINT [FK_Tipol_HotelTipol_ToTable]
    FOREIGN KEY ([idhotel])
    REFERENCES [dbo].[Hotel]
        ([idHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_HotelTipol_ToTable'
CREATE INDEX [IX_FK_Tipol_HotelTipol_ToTable]
ON [dbo].[Tipol_HotelTipol]
    ([idhotel]);
GO

-- Creating foreign key on [idhotel] in table 'Reduccion_Hotel'
ALTER TABLE [dbo].[Reduccion_Hotel]
ADD CONSTRAINT [FK_Tipol_ReduccTipol_ToTable]
    FOREIGN KEY ([idhotel])
    REFERENCES [dbo].[Hotel]
        ([idHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_ReduccTipol_ToTable'
CREATE INDEX [IX_FK_Tipol_ReduccTipol_ToTable]
ON [dbo].[Reduccion_Hotel]
    ([idhotel]);
GO

-- Creating foreign key on [idhotel] in table 'Suplemento_Hotel'
ALTER TABLE [dbo].[Suplemento_Hotel]
ADD CONSTRAINT [FK_Tipol_SupleTipol_ToTable]
    FOREIGN KEY ([idhotel])
    REFERENCES [dbo].[Hotel]
        ([idHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_SupleTipol_ToTable'
CREATE INDEX [IX_FK_Tipol_SupleTipol_ToTable]
ON [dbo].[Suplemento_Hotel]
    ([idhotel]);
GO

-- Creating foreign key on [idTemporadaVuelo] in table 'NomTempAvionClase'
ALTER TABLE [dbo].[NomTempAvionClase]
ADD CONSTRAINT [FK_NomTempClase_ToTable_1]
    FOREIGN KEY ([idTemporadaVuelo])
    REFERENCES [dbo].[TemporadaAvion]
        ([idTemporadaVuelo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NomTempClase_ToTable_1'
CREATE INDEX [IX_FK_NomTempClase_ToTable_1]
ON [dbo].[NomTempAvionClase]
    ([idTemporadaVuelo]);
GO

-- Creating foreign key on [idreduccion] in table 'NomTempRedHotel'
ALTER TABLE [dbo].[NomTempRedHotel]
ADD CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_]
    FOREIGN KEY ([idreduccion])
    REFERENCES [dbo].[Reduccion]
        ([idReduccion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temporada_Hotel_ToTable_'
CREATE INDEX [IX_FK_Nom_Temporada_Hotel_ToTable_]
ON [dbo].[NomTempRedHotel]
    ([idreduccion]);
GO

-- Creating foreign key on [idTemporada_Reduccion] in table 'NomTempRedHotel'
ALTER TABLE [dbo].[NomTempRedHotel]
ADD CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_1]
    FOREIGN KEY ([idTemporada_Reduccion])
    REFERENCES [dbo].[Temporada_Reduccion]
        ([idTemporada_Reduccion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temporada_Hotel_ToTable_1'
CREATE INDEX [IX_FK_Nom_Temporada_Hotel_ToTable_1]
ON [dbo].[NomTempRedHotel]
    ([idTemporada_Reduccion]);
GO

-- Creating foreign key on [idtipotarifa] in table 'NomTempRedHotel'
ALTER TABLE [dbo].[NomTempRedHotel]
ADD CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_2]
    FOREIGN KEY ([idtipotarifa])
    REFERENCES [dbo].[Tipo_Tarifa]
        ([idTipoTarifa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temporada_Hotel_ToTable_2'
CREATE INDEX [IX_FK_Nom_Temporada_Hotel_ToTable_2]
ON [dbo].[NomTempRedHotel]
    ([idtipotarifa]);
GO

-- Creating foreign key on [idSuplementoHotel] in table 'NomTempSupHotel'
ALTER TABLE [dbo].[NomTempSupHotel]
ADD CONSTRAINT [FK_Nom_Temp_Hotel_ToTable_6]
    FOREIGN KEY ([idSuplementoHotel])
    REFERENCES [dbo].[Temporada_Suplemento]
        ([idSuplementoHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temp_Hotel_ToTable_6'
CREATE INDEX [IX_FK_Nom_Temp_Hotel_ToTable_6]
ON [dbo].[NomTempSupHotel]
    ([idSuplementoHotel]);
GO

-- Creating foreign key on [idsuplemento] in table 'NomTempSupHotel'
ALTER TABLE [dbo].[NomTempSupHotel]
ADD CONSTRAINT [FK_Nom_Temp_Hotel_ToTable_7]
    FOREIGN KEY ([idsuplemento])
    REFERENCES [dbo].[Suplemento]
        ([idSuplemento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temp_Hotel_ToTable_7'
CREATE INDEX [IX_FK_Nom_Temp_Hotel_ToTable_7]
ON [dbo].[NomTempSupHotel]
    ([idsuplemento]);
GO

-- Creating foreign key on [idtipotarifa] in table 'NomTempSupHotel'
ALTER TABLE [dbo].[NomTempSupHotel]
ADD CONSTRAINT [FK_Nom_Temp_Hotel_ToTable_8]
    FOREIGN KEY ([idtipotarifa])
    REFERENCES [dbo].[Tipo_Tarifa]
        ([idTipoTarifa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temp_Hotel_ToTable_8'
CREATE INDEX [IX_FK_Nom_Temp_Hotel_ToTable_8]
ON [dbo].[NomTempSupHotel]
    ([idtipotarifa]);
GO

-- Creating foreign key on [idTemporadaHotel] in table 'NomTempTipHotel'
ALTER TABLE [dbo].[NomTempTipHotel]
ADD CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_6]
    FOREIGN KEY ([idTemporadaHotel])
    REFERENCES [dbo].[Temporada_Hotel]
        ([idTemporadaHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temporada_Hotel_ToTable_6'
CREATE INDEX [IX_FK_Nom_Temporada_Hotel_ToTable_6]
ON [dbo].[NomTempTipHotel]
    ([idTemporadaHotel]);
GO

-- Creating foreign key on [idTiologia] in table 'NomTempTipHotel'
ALTER TABLE [dbo].[NomTempTipHotel]
ADD CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_7]
    FOREIGN KEY ([idTiologia])
    REFERENCES [dbo].[Tipologia]
        ([idTiologia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temporada_Hotel_ToTable_7'
CREATE INDEX [IX_FK_Nom_Temporada_Hotel_ToTable_7]
ON [dbo].[NomTempTipHotel]
    ([idTiologia]);
GO

-- Creating foreign key on [idtipotarifa] in table 'NomTempTipHotel'
ALTER TABLE [dbo].[NomTempTipHotel]
ADD CONSTRAINT [FK_Nom_Temporada_Hotel_ToTable_8]
    FOREIGN KEY ([idtipotarifa])
    REFERENCES [dbo].[Tipo_Tarifa]
        ([idTipoTarifa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Nom_Temporada_Hotel_ToTable_8'
CREATE INDEX [IX_FK_Nom_Temporada_Hotel_ToTable_8]
ON [dbo].[NomTempTipHotel]
    ([idtipotarifa]);
GO

-- Creating foreign key on [idSubdestino] in table 'OficinaRenta'
ALTER TABLE [dbo].[OficinaRenta]
ADD CONSTRAINT [FK_OficinaRenta_ToTable]
    FOREIGN KEY ([idSubdestino])
    REFERENCES [dbo].[Subdestino]
        ([idSubdestino])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OficinaRenta_ToTable'
CREATE INDEX [IX_FK_OficinaRenta_ToTable]
ON [dbo].[OficinaRenta]
    ([idSubdestino]);
GO

-- Creating foreign key on [idOficina] in table 'OficPerCar'
ALTER TABLE [dbo].[OficPerCar]
ADD CONSTRAINT [FK_OficPerCar_ToTable_1]
    FOREIGN KEY ([idOficina])
    REFERENCES [dbo].[OficinaRenta]
        ([idOficina])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OficPerCar_ToTable_1'
CREATE INDEX [IX_FK_OficPerCar_ToTable_1]
ON [dbo].[OficPerCar]
    ([idOficina]);
GO

-- Creating foreign key on [idOrigenRental] in table 'Rental'
ALTER TABLE [dbo].[Rental]
ADD CONSTRAINT [FK_Rental_ToTable_1]
    FOREIGN KEY ([idOrigenRental])
    REFERENCES [dbo].[OrigenRental]
        ([idOrigenRental])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Rental_ToTable_1'
CREATE INDEX [IX_FK_Rental_ToTable_1]
ON [dbo].[Rental]
    ([idOrigenRental]);
GO

-- Creating foreign key on [idPaquete] in table 'TemporadaPaquete'
ALTER TABLE [dbo].[TemporadaPaquete]
ADD CONSTRAINT [FK_Temporada_Paquete_ToTable_1]
    FOREIGN KEY ([idPaquete])
    REFERENCES [dbo].[Paquete]
        ([idPaquete])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Paquete_ToTable_1'
CREATE INDEX [IX_FK_Temporada_Paquete_ToTable_1]
ON [dbo].[TemporadaPaquete]
    ([idPaquete]);
GO

-- Creating foreign key on [idrango] in table 'Rangos_Tarifa'
ALTER TABLE [dbo].[Rangos_Tarifa]
ADD CONSTRAINT [FK_Tipol_RangosTarifaCarro_ToTable_1]
    FOREIGN KEY ([idrango])
    REFERENCES [dbo].[Rango]
        ([idrango])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_RangosTarifaCarro_ToTable_1'
CREATE INDEX [IX_FK_Tipol_RangosTarifaCarro_ToTable_1]
ON [dbo].[Rangos_Tarifa]
    ([idrango]);
GO

-- Creating foreign key on [idcondtarifa] in table 'Rangos_Tarifa'
ALTER TABLE [dbo].[Rangos_Tarifa]
ADD CONSTRAINT [FK_Tipol_RangosTarifaCarro_ToTable]
    FOREIGN KEY ([idcondtarifa])
    REFERENCES [dbo].[TarifaCarro]
        ([idcondtarifa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_RangosTarifaCarro_ToTable'
CREATE INDEX [IX_FK_Tipol_RangosTarifaCarro_ToTable]
ON [dbo].[Rangos_Tarifa]
    ([idcondtarifa]);
GO

-- Creating foreign key on [idReduccion] in table 'Reduccion_Hotel'
ALTER TABLE [dbo].[Reduccion_Hotel]
ADD CONSTRAINT [FK_Tipol_ReduccTipol_ToTable_1]
    FOREIGN KEY ([idReduccion])
    REFERENCES [dbo].[Reduccion]
        ([idReduccion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_ReduccTipol_ToTable_1'
CREATE INDEX [IX_FK_Tipol_ReduccTipol_ToTable_1]
ON [dbo].[Reduccion_Hotel]
    ([idReduccion]);
GO

-- Creating foreign key on [idRental] in table 'TemporadaRental'
ALTER TABLE [dbo].[TemporadaRental]
ADD CONSTRAINT [FK_TemporadaRental_ToTable]
    FOREIGN KEY ([idRental])
    REFERENCES [dbo].[Rental]
        ([idRental])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaRental_ToTable'
CREATE INDEX [IX_FK_TemporadaRental_ToTable]
ON [dbo].[TemporadaRental]
    ([idRental]);
GO

-- Creating foreign key on [idSuplemento] in table 'Suplemento_Hotel'
ALTER TABLE [dbo].[Suplemento_Hotel]
ADD CONSTRAINT [FK_Tipol_SupleTipol_ToTable_1]
    FOREIGN KEY ([idSuplemento])
    REFERENCES [dbo].[Suplemento]
        ([idSuplemento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_SupleTipol_ToTable_1'
CREATE INDEX [IX_FK_Tipol_SupleTipol_ToTable_1]
ON [dbo].[Suplemento_Hotel]
    ([idSuplemento]);
GO

-- Creating foreign key on [idTemporada] in table 'TarifaCarro'
ALTER TABLE [dbo].[TarifaCarro]
ADD CONSTRAINT [FK_TarifaCarro_ToTable_2]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TarifaCarro_ToTable_2'
CREATE INDEX [IX_FK_TarifaCarro_ToTable_2]
ON [dbo].[TarifaCarro]
    ([idTemporada]);
GO

-- Creating foreign key on [idtipologia_carro] in table 'TarifaCarro'
ALTER TABLE [dbo].[TarifaCarro]
ADD CONSTRAINT [FK_TarifaCarro_ToTable_5]
    FOREIGN KEY ([idtipologia_carro])
    REFERENCES [dbo].[Tipologia_Carro]
        ([idtipologia_carro])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TarifaCarro_ToTable_5'
CREATE INDEX [IX_FK_TarifaCarro_ToTable_5]
ON [dbo].[TarifaCarro]
    ([idtipologia_carro]);
GO

-- Creating foreign key on [idTemporadaEmpresa] in table 'TarifaCarro'
ALTER TABLE [dbo].[TarifaCarro]
ADD CONSTRAINT [FK_TarifaCarro_ToTable_7]
    FOREIGN KEY ([idTemporadaEmpresa])
    REFERENCES [dbo].[TemporadaEmpresa]
        ([idTemporadaEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TarifaCarro_ToTable_7'
CREATE INDEX [IX_FK_TarifaCarro_ToTable_7]
ON [dbo].[TarifaCarro]
    ([idTemporadaEmpresa]);
GO

-- Creating foreign key on [idTarificacion] in table 'Tipologia'
ALTER TABLE [dbo].[Tipologia]
ADD CONSTRAINT [FK_Tipologia_ToTable]
    FOREIGN KEY ([idTarificacion])
    REFERENCES [dbo].[Tarificacion]
        ([idTarificacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipologia_ToTable'
CREATE INDEX [IX_FK_Tipologia_ToTable]
ON [dbo].[Tipologia]
    ([idTarificacion]);
GO

-- Creating foreign key on [idTemporada] in table 'TemporadaAvion'
ALTER TABLE [dbo].[TemporadaAvion]
ADD CONSTRAINT [FK_Temporada_Avion_ToTable_5]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Avion_ToTable_5'
CREATE INDEX [IX_FK_Temporada_Avion_ToTable_5]
ON [dbo].[TemporadaAvion]
    ([idTemporada]);
GO

-- Creating foreign key on [idTemporada] in table 'TemporadaExcursion'
ALTER TABLE [dbo].[TemporadaExcursion]
ADD CONSTRAINT [FK_Temporada_Excursion_ToTable_3]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Excursion_ToTable_3'
CREATE INDEX [IX_FK_Temporada_Excursion_ToTable_3]
ON [dbo].[TemporadaExcursion]
    ([idTemporada]);
GO

-- Creating foreign key on [idTemporada] in table 'Temporada_Hotel'
ALTER TABLE [dbo].[Temporada_Hotel]
ADD CONSTRAINT [FK_Temporada_Hotel_ToTable_5]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Hotel_ToTable_5'
CREATE INDEX [IX_FK_Temporada_Hotel_ToTable_5]
ON [dbo].[Temporada_Hotel]
    ([idTemporada]);
GO

-- Creating foreign key on [idTemporada] in table 'TemporadaPaquete'
ALTER TABLE [dbo].[TemporadaPaquete]
ADD CONSTRAINT [FK_Temporada_Paquete_ToTable_3]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Paquete_ToTable_3'
CREATE INDEX [IX_FK_Temporada_Paquete_ToTable_3]
ON [dbo].[TemporadaPaquete]
    ([idTemporada]);
GO

-- Creating foreign key on [idtemporada] in table 'Temporada_Reduccion'
ALTER TABLE [dbo].[Temporada_Reduccion]
ADD CONSTRAINT [FK_Temporada_Reduccion_ToTable_5]
    FOREIGN KEY ([idtemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Reduccion_ToTable_5'
CREATE INDEX [IX_FK_Temporada_Reduccion_ToTable_5]
ON [dbo].[Temporada_Reduccion]
    ([idtemporada]);
GO

-- Creating foreign key on [idTemporada] in table 'Temporada_Suplemento'
ALTER TABLE [dbo].[Temporada_Suplemento]
ADD CONSTRAINT [FK_Temporada_Suplemento_ToTable_5]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Temporada_Suplemento_ToTable_5'
CREATE INDEX [IX_FK_Temporada_Suplemento_ToTable_5]
ON [dbo].[Temporada_Suplemento]
    ([idTemporada]);
GO

-- Creating foreign key on [idTemporada] in table 'TemporadaEmpresa'
ALTER TABLE [dbo].[TemporadaEmpresa]
ADD CONSTRAINT [FK_TemporadaEmpresa_ToTable_5]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaEmpresa_ToTable_5'
CREATE INDEX [IX_FK_TemporadaEmpresa_ToTable_5]
ON [dbo].[TemporadaEmpresa]
    ([idTemporada]);
GO

-- Creating foreign key on [idTemporada] in table 'TemporadaRental'
ALTER TABLE [dbo].[TemporadaRental]
ADD CONSTRAINT [FK_TemporadaRental_ToTable_3]
    FOREIGN KEY ([idTemporada])
    REFERENCES [dbo].[Temporada]
        ([idtemporada])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TemporadaRental_ToTable_3'
CREATE INDEX [IX_FK_TemporadaRental_ToTable_3]
ON [dbo].[TemporadaRental]
    ([idTemporada]);
GO

-- Creating foreign key on [idTiologia] in table 'Tipol_HotelTipol'
ALTER TABLE [dbo].[Tipol_HotelTipol]
ADD CONSTRAINT [FK_Tipol_HotelTipol_ToTable_1]
    FOREIGN KEY ([idTiologia])
    REFERENCES [dbo].[Tipologia]
        ([idTiologia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Tipol_HotelTipol_ToTable_1'
CREATE INDEX [IX_FK_Tipol_HotelTipol_ToTable_1]
ON [dbo].[Tipol_HotelTipol]
    ([idTiologia]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------