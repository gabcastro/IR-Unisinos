# Information Retrieval

# Contributors
|<img src="https://avatars.githubusercontent.com/u/15840469?v=4" width=150px height=150px><br> [Gabriel Castro](https://github.com/gabcastro) | <img src="https://avatars.githubusercontent.com/u/22084856?v=4" width=150px height=150px><br> [Paulo Backes](https://github.com/JrBackes)| <img src="https://avatars1.githubusercontent.com/u/43481916?s=400&u=2683d479631afcd710a45ec6cae3e82ba1a846bf&v=4" width=150px height=150px><br> [Vitor Matter](https://github.com/vmatter) |
|---|---|--|

# Application data
The project's premise is to use a search String informed by the user, to find the words in a file in PDF format.

# Business rules
- The words that are located in the text will be informed in lowercase.
- In the case of multiple words, the logical operator between them must be informed. The accepted logical operators will be **AND** and **OR**.
- Operators must be written in capital letters, while the words consulted in small letters.

The application should return the following results, based on the query performed.
- If OR operator is used, the application must return the number of occurrences of each of the queried words.
- If AND operator is used, the application must return if the two words were found in the text, together with the number of occurrences of each one of them.
It is important to note that the words may appear in the document in more than one format. For example, the word aplicação can also appear as Aplicação, APLICAÇÃO, aplicacao, etc. 


# Next steps

 - TODO: Implement stemming logic

