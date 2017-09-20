CREATE DATABASE inventario;

CREATE TABLE usuario(
	id SERIAL NOT NULL PRIMARY KEY,
	nome VARCHAR(50) NOT NULL UNIQUE,
	prontuario VARCHAR(20) NOT NULL UNIQUE,
	email VARCHAR(50),
	nivel INTEGER,
	senha VARCHAR(18)
);

  CREATE TABLE localidade(
  	id SERIAL NOT NULL PRIMARY KEY,
  	nome VARCHAR(50) NOT NULL UNIQUE,
  	categoria INTEGER,
  	bloco VARCHAR(50)
  );

  CREATE TABLE item(
    id SERIAL NOT NULL PRIMARY KEY,
    patrimonio VARCHAR(50) NOT NULL UNIQUE,
    categoria INTEGER,
    subcategoria INTEGER,
    localidade INTEGER,
    status INTEGER
  );

  CREATE TABLE localidade_categoria(
    id SERIAL NOT NULL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE,
  );

  CREATE TABLE item_categoria(
    id SERIAL NOT NULL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE,
  );

  CREATE TABLE item_subcategoria(
    id SERIAL NOT NULL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE,
  );

  CREATE TABLE item_status(
    id SERIAL NOT NULL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE,
  );

-- CONSTRAINTS
ALTER TABLE localidade ADD CONSTRAINT fk_localidade_categoria FOREIGN KEY(categoria) REFERENCES localidade_categoria(id);

ALTER TABLE item ADD CONSTRAINT fk_item_localidade FOREIGN KEY(localidade) REFERENCES localidade(id);
ALTER TABLE item ADD CONSTRAINT fk_item_categoria FOREIGN KEY(categoria) REFERENCES item_categoria(id);
ALTER TABLE item ADD CONSTRAINT fk_item_subcategoria FOREIGN KEY(subcategoria) REFERENCES item_subcategoria(id);
ALTER TABLE item ADD CONSTRAINT fk_item_status FOREIGN KEY(status) REFERENCES item_status(id);
