
-- Queries to fill the 'update_frequency' table --

INSERT INTO `usage_data_in_sweden_simple`.`update_frequency` (`id_update_frequency`, `update_frequency_name`) VALUES ('1', 'No update / Only one upload');
INSERT INTO `usage_data_in_sweden_simple`.`update_frequency` (`id_update_frequency`, `update_frequency_name`) VALUES ('2', 'Yearly update');
INSERT INTO `usage_data_in_sweden_simple`.`update_frequency` (`id_update_frequency`, `update_frequency_name`) VALUES ('3', 'Monthly update');
INSERT INTO `usage_data_in_sweden_simple`.`update_frequency` (`id_update_frequency`, `update_frequency_name`) VALUES ('4', 'Weekly update');
INSERT INTO `usage_data_in_sweden_simple`.`update_frequency` (`id_update_frequency`, `update_frequency_name`) VALUES ('5', 'Daily update');

-- Queries to fill the 'data_language' table --

INSERT INTO `usage_data_in_sweden_simple`.`data_language` (`id_data_language`, `data_language_name`) VALUES ('1', 'Svenska');
INSERT INTO `usage_data_in_sweden_simple`.`data_language` (`id_data_language`, `data_language_name`) VALUES ('2', 'English');

-- Queries to fill the 'data_theme' table --

INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Befolkning och samhälle / Population and society');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Ekonomi och finans / Economy and finance');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Utbildning, kultur och sport / Education, culture and sport');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Miljö / Environment');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Regeringen och den offentliga sektorn / Government and public sector');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Regioner och städer / Regions and cities');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Energi / Energy');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Vetenskap och teknik / Science and technology');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Jordbruk, fiske, skogsbruk och livsmedel / Agriculture, fisheries, forestry and food');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Hälsa / Health');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Transport / Transport');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Internationella frågor / International issues');
INSERT INTO `usage_data_in_sweden_simple`.`data_theme` (`data_theme_name`) VALUES ('Rättvisa, rättsliga system och allmän säkerhet / Justice, legal system and public safety');

-- Queries to fill the 'data_format' table --

INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/json');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('text/csv');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('text/html');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/vnd.ogc.wms_xml');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/vnd.ogc.wfs_xml');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/rdf+xml');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/zip');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/x-shapefile');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/pdf');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/json+zip');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/vnd.ms-excel');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/gml+xml');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/vnd.google-earth.kml+xml');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('application/geopackage+sqlite3');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('text/csv+zip');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('text/plain');
INSERT INTO `usage_data_in_sweden_simple`.`data_format` (`data_format_name`) VALUES ('api');

-- Queries to fill the 'data_owner' table --

INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Statistikmyndigheten SCB - Statistiska centralbyrån');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Pensionsmyndigheten');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Stockholms stad');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Trafikverket');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Umeå Energi');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Styrelsen för ackreditering och teknisk kontroll, Swedac');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('SMHI - Öppna Data');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Naturvårdsverket');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Huddinge kommun');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Malmö stad');
INSERT INTO `usage_data_in_sweden_simple`.`data_owner` (`data_owner_name`) VALUES ('Energimyndigheten');

-- Queries to fill the 'open_data' table --

INSERT INTO `usage_data_in_sweden_simple`.`open_data` (`data_url`, `data_open_license`, `data_owner_id`, `update_frequency_id`, `data_theme_id`) VALUES ('https://www.dataportal.se/sv/datasets/91_57310/taxeringsenheter-typkod-220-221-genomsnittliga-varden-och-arealer-efter-region-typkod-och-strandzonsklass-ar-2003-2006-2009-2012-2015-2003-2021#ref=?p=1&q=&s=2&t=20&f=http%3A%2F%2Fpurl.org%2Fdc%2Fterms%2Fpublisher%7C%7Chttp%3A%2F%2Fid.kb.se%2Forganisations%2FSE2021000837%7C%7Cfalse%7C%7Curi%7C%7COrganisation%7C%7CStatistikmyndigheten%20SCB%20-%20Statistiska%20centralbyr%C3%A5n&rt=dataset%24esterms_IndependentDataService%24esterms_ServedByDataService&c=false', '1', '1', '2', '6');
INSERT INTO `usage_data_in_sweden_simple`.`open_data` (`data_url`, `data_open_license`, `data_owner_id`, `update_frequency_id`, `data_theme_id`) VALUES ('https://www.dataportal.se/sv/datasets/778_21912/viltolyckskartor-ren-2016-2020#ref=?p=1&q=&s=2&t=20&f=http%3A%2F%2Fpurl.org%2Fdc%2Fterms%2Fpublisher%7C%7Chttp%3A%2F%2Fid.kb.se%2Forganisations%2FSE2021000837%7C%7Cfalse%7C%7Curi%7C%7COrganisation%7C%7CStatistikmyndigheten%20SCB%20-%20Statistiska%20centralbyr%C3%A5n%24http%3A%2F%2Fpurl.org%2Fdc%2Fterms%2Fpublisher%7C%7Chttps%3A%2F%2Fwww.geodata.se%3A%2Fgeodataportalen%2Fperson%2FTrafikverket%7C%7Cfalse%7C%7Curi%7C%7COrganisation%7C%7CTrafikverket&rt=dataset%24esterms_IndependentDataService%24esterms_ServedByDataService&c=false', '0', '4', '1', '11');

-- Queries to fill the 'data_usage' table -- 
INSERT INTO `usage_data_in_sweden_simple`.`data_usage` (`open_data_id`, `date_of_usage`, `data_format_id`, `language_id`, `is_downloaded`) VALUES ('1', '2020-01-19 19:55:33', '18', '1', '0');
INSERT INTO `usage_data_in_sweden_simple`.`data_usage` (`open_data_id`, `date_of_usage`, `data_format_id`, `language_id`, `is_downloaded`) VALUES ('1', '2020-09-02 13:43:10', '18', '2', '0');
INSERT INTO `usage_data_in_sweden_simple`.`data_usage` (`open_data_id`, `date_of_usage`, `data_format_id`, `language_id`, `is_downloaded`) VALUES ('2', '2022-08-10 07:08:02', '10', '1', '1');


