-- ('User' vervangen voor 'AppUser' om SQL Server's conflicts te vermijden)
CREATE TABLE AppUser (
    user_id INT IDENTITY(1,1) PRIMARY KEY, -- IDENTITY(1,1) zorgt ervoor dat de user_id automatisch wordt ingevuld, zelfde als AUTO_INCREMENT in MySQL
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL
);
CREATE TABLE MovieGenre (
    genre_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    description TEXT
);
CREATE TABLE Director (
    director_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    birthdate DATE,
    nationality VARCHAR(50)
);
CREATE TABLE Movie (
    movie_id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    length INT,
    budget DECIMAL(15, 2),
    director_id INT,
    genre_id INT,
    FOREIGN KEY (director_id) REFERENCES Director(director_id),
    FOREIGN KEY (genre_id) REFERENCES MovieGenre(genre_id)
);
CREATE TABLE Watched (
    user_id INT,
    movie_id INT,
    PRIMARY KEY (user_id, movie_id),
    FOREIGN KEY (user_id) REFERENCES AppUser(user_id),
    FOREIGN KEY (movie_id) REFERENCES Movie(movie_id)
);
CREATE TABLE IMDB_profile (
    IMDB_id INT IDENTITY(1,1) PRIMARY KEY,
    status VARCHAR(20),
    date_last_modification DATE,
    movie_id INT,
    FOREIGN KEY (movie_id) REFERENCES Movie(movie_id)
);
CREATE TABLE Award (
    award_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    year INT,
    report TEXT,
    movie_id INT,
    FOREIGN KEY (movie_id) REFERENCES Movie(movie_id)
);
