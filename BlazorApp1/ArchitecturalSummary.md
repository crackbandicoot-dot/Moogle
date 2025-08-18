The architecture is designed for a Blazor-based offline text search engine UI.

 ====================================================================================
                                 Architectural Summary
 ====================================================================================

 The architecture is divided into three main layers: Core Models, Services, and UI Components.

 ------------------------------------------------------------------------------------
 1. Core Models:
    These are simple C# classes (POCOs) that represent the data entities of the application.
    - SearchConfiguration: Holds all user-configurable settings, designed to be serialized to/from JSON.
    - SearchResult: Represents a single item in the search results list, containing a snippet and an ID.
    - Document: Represents a full document with its ID, title, and complete content.

 ------------------------------------------------------------------------------------
 2. Services (Interfaces):
    These define the contracts for business logic and data access, abstracting the UI from the implementation details.
    - IConfigurationService: Defines methods for loading and saving the `SearchConfiguration` from a JSON file.
    - ISearchService: Defines methods for performing searches and retrieving full documents. This service acts as a bridge to the (unimplemented) offline search engine logic.

 ------------------------------------------------------------------------------------
 3. UI Components (.razor):
    These are the Blazor components that form the user interface. They are designed to be modular and reusable.
    - SearchBar.razor: A component for user query input.
    - SearchResultItem.razor: Displays a single search result.
    - SearchResults.razor: Displays a list of search results and handles pagination.
    - DocumentView.razor: Displays the content of a single opened document.
    - TabbedView.razor: A container that manages and displays multiple `DocumentView` components in a tabbed interface.
    -Configuration.razor: A form for viewing and editing application settings.
    - SearchPage.razor: A routable page that combines the `SearchBar` and `SearchResults` to create the main search experience.
    - MainLayout.razor: The main application layout. It will manage the state of open document tabs and orchestrate the interaction between the search page and the document view.

 ====================================================================================
 The following files will define each of these types in detail.
 A developer's task is to inject the services and implement the method bodies.
 ====================================================================================
