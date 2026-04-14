-- init_db.sql : Script de création de la base de données TourneeFutee
-- À exécuter avant de lancer les tests d'intégration PersistanceTests.
--
-- Usage :
--   mysql -u root -p < init_db.sql
--
-- Crée la base principale (tourneefutee) et la base de test (tourneefutee_test).
-- Ce script cible la base de TEST (utilisée par PersistanceTests).

CREATE DATABASE IF NOT EXISTS tourneefutee
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

CREATE DATABASE IF NOT EXISTS tourneefutee_test
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- ─────────────────────────────────────────────────────────────────────────────
-- Tables de la base principale
-- ─────────────────────────────────────────────────────────────────────────────

USE tourneefutee;

DROP TABLE IF EXISTS EtapeTournee;
DROP TABLE IF EXISTS Tournee;
DROP TABLE IF EXISTS Arc;
DROP TABLE IF EXISTS Sommet;
DROP TABLE IF EXISTS Graphe;

CREATE TABLE Graphe (
    id         INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nb_sommets INT          NOT NULL,
    oriente    TINYINT(1)   NOT NULL
);

CREATE TABLE Sommet (
    id        INT UNSIGNED  AUTO_INCREMENT PRIMARY KEY,
    nom       VARCHAR(255)  NOT NULL,
    valeur    FLOAT         NOT NULL,
    graphe_id INT UNSIGNED  NOT NULL,
    ordre     INT           NOT NULL,
    FOREIGN KEY (graphe_id) REFERENCES Graphe(id) ON DELETE CASCADE
);

CREATE TABLE Arc (
    id             INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    source_id      INT UNSIGNED NOT NULL,
    destination_id INT UNSIGNED NOT NULL,
    poids          FLOAT        NOT NULL,
    graphe_id      INT UNSIGNED NOT NULL,
    FOREIGN KEY (source_id)      REFERENCES Sommet(id) ON DELETE CASCADE,
    FOREIGN KEY (destination_id) REFERENCES Sommet(id) ON DELETE CASCADE,
    FOREIGN KEY (graphe_id)      REFERENCES Graphe(id) ON DELETE CASCADE
);

CREATE TABLE Tournee (
    id          INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    cout_total  FLOAT        NOT NULL,
    graphe_id   INT UNSIGNED NOT NULL,
    FOREIGN KEY (graphe_id) REFERENCES Graphe(id) ON DELETE CASCADE
);

CREATE TABLE EtapeTournee (
    tournee_id INT UNSIGNED NOT NULL,
    sommet_id  INT UNSIGNED NOT NULL,
    ordre      INT          NOT NULL,
    PRIMARY KEY (tournee_id, ordre),
    FOREIGN KEY (tournee_id) REFERENCES Tournee(id) ON DELETE CASCADE,
    FOREIGN KEY (sommet_id)  REFERENCES Sommet(id)  ON DELETE CASCADE
);

-- ─────────────────────────────────────────────────────────────────────────────
-- Tables de la base de TEST (même structure)
-- ─────────────────────────────────────────────────────────────────────────────

USE tourneefutee_test;

DROP TABLE IF EXISTS EtapeTournee;
DROP TABLE IF EXISTS Tournee;
DROP TABLE IF EXISTS Arc;
DROP TABLE IF EXISTS Sommet;
DROP TABLE IF EXISTS Graphe;

CREATE TABLE Graphe (
    id         INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nb_sommets INT          NOT NULL,
    oriente    TINYINT(1)   NOT NULL
);

CREATE TABLE Sommet (
    id        INT UNSIGNED  AUTO_INCREMENT PRIMARY KEY,
    nom       VARCHAR(255)  NOT NULL,
    valeur    FLOAT         NOT NULL,
    graphe_id INT UNSIGNED  NOT NULL,
    ordre     INT           NOT NULL,
    FOREIGN KEY (graphe_id) REFERENCES Graphe(id) ON DELETE CASCADE
);

CREATE TABLE Arc (
    id             INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    source_id      INT UNSIGNED NOT NULL,
    destination_id INT UNSIGNED NOT NULL,
    poids          FLOAT        NOT NULL,
    graphe_id      INT UNSIGNED NOT NULL,
    FOREIGN KEY (source_id)      REFERENCES Sommet(id) ON DELETE CASCADE,
    FOREIGN KEY (destination_id) REFERENCES Sommet(id) ON DELETE CASCADE,
    FOREIGN KEY (graphe_id)      REFERENCES Graphe(id) ON DELETE CASCADE
);

CREATE TABLE Tournee (
    id          INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    cout_total  FLOAT        NOT NULL,
    graphe_id   INT UNSIGNED NOT NULL,
    FOREIGN KEY (graphe_id) REFERENCES Graphe(id) ON DELETE CASCADE
);

CREATE TABLE EtapeTournee (
    tournee_id INT UNSIGNED NOT NULL,
    sommet_id  INT UNSIGNED NOT NULL,
    ordre      INT          NOT NULL,
    PRIMARY KEY (tournee_id, ordre),
    FOREIGN KEY (tournee_id) REFERENCES Tournee(id) ON DELETE CASCADE,
    FOREIGN KEY (sommet_id)  REFERENCES Sommet(id)  ON DELETE CASCADE
);
