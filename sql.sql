--SQL projeto inventarioIFSP

--usuario

CREATE TABLE usuario(
	id SERIAL NOT NULL PRIMARY KEY,
	matricula VARCHAR(20) NOT NULL UNIQUE, --tamanho?
	nome VARCHAR(50) NOT NULL UNIQUE,
	email VARCHAR(50),
	telefone1 VARCHAR(18),
	telefone2 VARCHAR(18)
);

/* crud
INSERT INTO usuario (matricula, nome, email, telefone1, telefone2)
	VALUES (@matricula, @nome, @email, @telefone1, @telefone2);

UPDATE usuario SET
	matricula = @matricula,
	nome = @nome,
	email = @email,
	telefone1 = @telefone1,
	telefone2 = @telefone2
WHERE id = @id;

DELETE FROM usuario WHERE id = @id;
*/
---------------------------------------------------------------------


--usuario_permissão  *diz quais usuários tem acesso a quais funcionalidades
-- essa tabela não precisa criar pasta nem crud...

CREATE TABLE usuario_permissao(
	permissao INTEGER NOT NULL, -- definir índice de permissoes
	usuario INTEGER NOT NULL
);
ALTER TABLE usuario_permissao ADD CONSTRAINT fk_usu_perm_usu FOREIGN KEY(usuario) REFERENCES usuario(id);
---------------------------------------------------------------------

-- item_categoria
CREATE TABLE item_categoria(
	id SERIAL NOT NULL PRIMARY KEY,
	nome VARCHAR(20) NOT NULL UNIQUE,
	descricao VARCHAR NOT NULL
);

/* crud
INSERT INTO item_categoria (nome, descricao)
	VALUES (@nome, @descricao);

UPDATE item_categoria SET
	nome = @nome,
	email = @descricao
WHERE id = @id;

DELETE FROM item_categoria WHERE id = @id;
*/
---------------------------------------------------------------------

-- item_sub_categoria
CREATE TABLE item_sub_categoria(
	id SERIAL NOT NULL PRIMARY KEY,
	nome VARCHAR(20) NOT NULL UNIQUE,
	descricao VARCHAR NOT NULL,
	categoria INTEGER NOT NULL
);
ALTER TABLE item_sub_categoria ADD CONSTRAINT fk_cat_sub_cat FOREIGN KEY(categoria) REFERENCES categoria(id);

/* crud
INSERT INTO item_sub_categoria (nome, descricao, categoria)
	VALUES (@nome, @descricao, @categoria);

UPDATE item_sub_categoria SET
	nome = @nome,
	email = @descricao,
	categoria = @categoria
WHERE id = @id;

DELETE FROM item_sub_categoria WHERE id = @id;
*/
---------------------------------------------------------------------

-- item
CREATE TABLE item(
	id SERIAL NOT NULL PRIMARY KEY,
	patrimonio VARCHAR(20) NOT NULL UNIQUE, --tamanho?
	sub_categoria INTEGER NOT NULL,
	sala_atual INTEGER,
	sala_anterior INTEGER,
	status INTEGER NOT NULL -- definir enumeração de status
);
ALTER TABLE item ADD CONSTRAINT fk_sala_anterior FOREIGN KEY(sala_anterior) REFERENCES sala(id);
ALTER TABLE item ADD CONSTRAINT fk_sala_atual FOREIGN KEY(sala_atual) REFERENCES sala(id);

/* crud
INSERT INTO item (patrimonio, sub_categoria)
	VALUES (@patrimonio, @sub_categoria);

UPDATE item SET
	patrimonio = @patrimonio,
	sub_categoria = @sub_categoria
WHERE id = @id;

DELETE FROM item WHERE id = @id;
*/
---------------------------------------------------------------------

-- sala_categoria
CREATE TABLE sala_categoria(
	id SERIAL NOT NULL PRIMARY KEY,
	nome VARCHAR(20) NOT NULL UNIQUE,
	descricao VARCHAR NOT NULL
);

/* crud
INSERT INTO sala_categoria (nome, descricao)
	VALUES (@nome, @descricao);

UPDATE sala_categoria SET
	nome = @nome,
	email = @descricao
WHERE id = @id;

DELETE FROM sala_categoria WHERE id = @id;
*/
---------------------------------------------------------------------

--sala
CREATE TABLE sala(
	id SERIAL NOT NULL PRIMARY KEY,
	bloco CHAR(1) NOT NULL,
	numero INTEGER NOT NULL,
	categoria INTEGER NOT NULL,
	capacidade INTEGER DEFAULT 0
);
ALTER TABLE sala ADD CONSTRAINT fk_sala_cat FOREIGN KEY(categoria) REFERENCES sala_categoria(id);

/* crud
INSERT INTO sala (bloco, numero, categoria, capacidade)
	VALUES (@bloco, @numero, @categoria, @capacidade);

UPDATE sala SET
	bloco = @bloco,
	numero = @numero,
	categoria = @categoria,
	capacidade = @capacidade
WHERE id = @id;

DELETE FROM sala WHERE id = @id;
*/
---------------------------------------------------------------------