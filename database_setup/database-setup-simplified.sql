-- Database creation --

CREATE DATABASE IF NOT EXISTS usage_data_in_sweden_simple DEFAULT CHARSET=utf8mb4 COLLATE utf8mb4_general_ci;

-- SELECTION OF DATABASE 'usage_data_in_sweden_simple' FOR FOLLOWING OPERATIONS

USE usage_data_in_sweden_simple;

-- CREATION OF TABLE 'data_owner' --

CREATE TABLE IF NOT EXISTS data_owner ( 
    id_data_owner BIGINT NOT NULL AUTO_INCREMENT,
    data_owner_name VARCHAR(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    PRIMARY KEY (id_data_owner)
)ENGINE = InnoDB;

-- CREATION OF TABLE 'update_frequency' --

CREATE TABLE IF NOT EXISTS update_frequency (
    id_update_frequency INT NOT NULL AUTO_INCREMENT,
    update_frequency_name VARCHAR(250) NOT NULL,
    PRIMARY KEY (id_update_frequency)
)ENGINE = InnoDB;

-- CREATION OF TABLE 'data_theme' --

CREATE TABLE IF NOT EXISTS data_theme(
    id_data_theme INT NOT NULL AUTO_INCREMENT,
    data_theme_name VARCHAR(250) NOT NULL,
    PRIMARY KEY (id_data_theme)
)ENGINE = InnoDB;

-- CREATION OF TABLE 'data_format' --

CREATE TABLE IF NOT EXISTS data_format(
    id_data_format INT NOT NULL AUTO_INCREMENT,
    data_format_name VARCHAR(250) NOT NULL,
    PRIMARY KEY (id_data_format)
)ENGINE = InnoDB;

-- CREATION OF TABLE 'language' --

CREATE TABLE IF NOT EXISTS data_language(
    id_data_language INT NOT NULL AUTO_INCREMENT,
    data_language_name VARCHAR(250) NOT NULL,
    PRIMARY KEY (id_data_language)
)ENGINE = InnoDB;

-- CREATION OF THE TABLE 'open_data' --

CREATE TABLE IF NOT EXISTS open_data(
    id_data BIGINT NOT NULL AUTO_INCREMENT,
    data_url TEXT NOT NULL,
    data_open_license TINYINT NOT NULL,
    data_owner_id BIGINT NOT NULL,
    update_frequency_id INT NOT NULL,
    data_theme_id INT NOT NULL,
    PRIMARY KEY (id_data),
    FOREIGN KEY (data_owner_id) REFERENCES data_owner (id_data_owner),
    FOREIGN KEY (update_frequency_id) REFERENCES update_frequency (id_update_frequency),
    FOREIGN KEY (data_theme_id) REFERENCES data_theme (id_data_theme),
    INDEX index_data_owner_id (data_owner_id),
    INDEX index_update_frequency_id (update_frequency_id),
    INDEX index_data_theme_id (data_theme_id)
)ENGINE = InnoDB;

-- CREATION OF THE TABLE 'data_usage' --

CREATE TABLE IF NOT EXISTS data_usage(
    id_data_usage BIGINT NOT NULL AUTO_INCREMENT,
    open_data_id BIGINT NOT NULL,
    date_of_usage DATETIME NOT NULL,
    data_format_id INT NOT NULL,
    language_id INT NOT NULL,
    is_downloaded TINYINT NOT NULL,
    PRIMARY KEY (id_data_usage),
    FOREIGN KEY (open_data_id) REFERENCES open_data(id_data),
    FOREIGN KEY (data_format_id) REFERENCES data_format(id_data_format),
    FOREIGN KEY (language_id) REFERENCES data_language(id_data_language),
    INDEX index_open_data_id (open_data_id),
    INDEX index_data_format_id (data_format_id),
    INDEX index_language_id (language_id)
)ENGINE = InnoDB;