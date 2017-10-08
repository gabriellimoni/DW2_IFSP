CREATE TABLE usuario(
	id SERIAL NOT NULL PRIMARY KEY,
	nome VARCHAR(50) NOT NULL,
	prontuario VARCHAR(20) NOT NULL UNIQUE,
	email VARCHAR(50) UNIQUE,
	nivel INTEGER DEFAULT 2,
	senha VARCHAR(255)
);

  CREATE TABLE localidade(
  	id SERIAL NOT NULL PRIMARY KEY,
  	nome VARCHAR(50) NOT NULL UNIQUE,
  	categoria INTEGER,
  	bloco VARCHAR(20)
  );

  CREATE TABLE item(
    id SERIAL NOT NULL PRIMARY KEY,
    patrimonio VARCHAR(20) NOT NULL UNIQUE,
    categoria INTEGER,
    localidade INTEGER,
    observacao VARCHAR(255),
    status INTEGER DEFAULT 0
  );

  CREATE TABLE localidade_categoria(
    id SERIAL NOT NULL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE,
    descricao VARCHAR(255)
  );

  CREATE TABLE item_categoria(
    id SERIAL NOT NULL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE,
    descricao VARCHAR(255)
  );

  CREATE TABLE item_status(
    id SERIAL NOT NULL PRIMARY KEY, 
    nome VARCHAR(50) NOT NULL UNIQUE,
    descricao VARCHAR(255)
  );

  CREATE TABLE inventario(
    id SERIAL NOT NULL PRIMARY KEY, 
    ano INTEGER NOT NULL,
    semestre INTEGER NOT NULL,
    consolidado VARCHAR(10) DEFAULT 'Não'
  );

  CREATE TABLE item_inventario(
    id SERIAL NOT NULL PRIMARY KEY, 
    inventario SERIAL NOT NULL , 
    patrimonio VARCHAR(50) NOT NULL,
    categoria VARCHAR(50) NOT NULL,
    localidade VARCHAR(50) NOT NULL, 
    bloco VARCHAR(20) NOT NULL, 
    status VARCHAR(50) NOT NULL
  );

-- CONSTRAINTS
ALTER TABLE localidade ADD CONSTRAINT fk_localidade_categoria FOREIGN KEY(categoria) REFERENCES localidade_categoria(id);

ALTER TABLE item ADD CONSTRAINT fk_item_localidade FOREIGN KEY(localidade) REFERENCES localidade(id);
ALTER TABLE item ADD CONSTRAINT fk_item_categoria FOREIGN KEY(categoria) REFERENCES item_categoria(id);
ALTER TABLE item ADD CONSTRAINT fk_item_status FOREIGN KEY(status) REFERENCES item_status(id);

ALTER TABLE item_inventario ADD CONSTRAINT fk_item_inventario FOREIGN KEY(inventario) REFERENCES inventario(id) ON DELETE CASCADE ON UPDATE CASCADE;
