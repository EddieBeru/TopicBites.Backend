# TopicBites
TopicBites is a collection of bite-sized articles on various topics, designed to be quick and easy to read. Each article is a self-contained piece that can be read in a few minutes.

The idea behind this project is giving developers a quick tool to implement on their .NET projects.

Behind this project, there is a college student who is trying to learn more about .NET and C# alongside helping himself and others with their studies.

By making this kind of structured articles, I hope to help others learn and grow in their understanding of various topics.

# Contributing
If you would like to contribute to TopicBites, please feel free to submit a pull request or open an issue. Contributions are welcome and appreciated!

# Usage
## TopicBites class
Each TopicBite object represents a single article. It contains the following properties:
- `Id`: A unique identifier for the article.
- `Title`: The title of the article.
- `Content`: The content of the article, which is a collection of `ContentBlock`s that represent all the info the article contains.

## Tree class
A tree is the main structure of the articles. It contains the following properties:
- `ParentId`: The ID of the parent node in the tree. Represent a pointer to get the parent node.
- `ItemId`: The ID of the current node in the tree. Represent a pointer to get the current node.
- `Children`: A collection of child nodes in the tree. Used to navigate down the different branches.

There is also `Parent` and `Item` properties that return the parent and current nodes respectively, allowing for easy navigation through the tree. 
These ones use recursive methods to find the parent and current nodes.


