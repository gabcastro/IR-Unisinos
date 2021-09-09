# Information Retrieval

# Contributors
|<img src="https://avatars.githubusercontent.com/u/15840469?v=4" width=150px height=150px><br> [Gabriel Castro](https://github.com/gabcastro) | <img src="https://avatars.githubusercontent.com/u/22084856?v=4" width=150px height=150px><br> [Paulo Backes](https://github.com/JrBackes)| <img src="https://avatars1.githubusercontent.com/u/43481916?s=400&u=2683d479631afcd710a45ec6cae3e82ba1a846bf&v=4" width=150px height=150px><br> [Vitor Matter](https://github.com/vmatter) |
|---|---|--|

# Application data
The project's premise is implement a search engine application with the objective of do a ranking of documents according to their similarity with a string informed by the user.

# Business rules
- The application must read all PDF documents located in the location informed by the user and create the vector space model (Vector Space).
- All the words located in the text will be informed in lowercase.
- The application should return the following results, based on the query performed.
- The project consists of implementing a search engine application with the objective of producing a ranking of documents according to their similarity with a string informed by the user.
- All words entered must be used as a basis for creating the ranking, that is, the application must consider the use of the AND operator. Important: the operator does not need to be informed and the words that make up the string can only be separated by spaces.
- The creation of the vector space must consider the removal of stop words and the reduction of each term to its stem (stemming).
- In order to facilitate the creation of the ranking, the inclusion of the vocabulary of each document in the vector space must consider the number of occurrences of each term.
- The application must contain an option to save the vector space in file in CSV format.
- The application must store each query in the SQLite database. The data stored are: date, time, search string, ranking of documents and similarity value of each document.
- The application should display the ranking of documents on screen, along with the similarity with the search string. If two or more documents have the same similarity score, they can be presented in any order (considering only the position of documents with the same score!).
