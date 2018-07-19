# Game of Life Application
<br>

Game of Life is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input. One interacts with the Game of Life by creating an initial configuration and observing how it evolves, or, for advanced players, by creating patterns with particular properties

The application consists of a "public" front-end developed using MVC5, and an internal administration website developed using Web Forms.

#### Development highlights:<br>
- Entity Framework employed for SQL database development and data operations<br>
- The Server pushes real-time updates to the browser via Signal R<br>
<br>
##### The application implements the following features:<br>
###### Unregistered users<br>
- List all Game templates in the system<br>
- Search for a Game Template by name<br>
- Create Multiple Games that are saved to the session<br>
- Delete Game from the session<br>
- Play a Game saved in the session<br>
- Register an account in the system<br>
<br>
###### Registered users<br>
- Login/Logout<br>
- Create Game Template<br>
- List/Delete Templates associated with user account<br>
- Save Games<br>
- List/Delete saved Games associated with user account<br>
- Play saved Game<br>
<br>
##### Security<br>
- The application stores user passwords in encrypted form using a Blowfish hash.<br>
<br>
##### Administration Site features:<br>
- Admin login/logout<br>
- List users/templates<br>
- Delete user/template<br>
- Upload template
