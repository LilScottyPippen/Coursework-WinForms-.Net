# Coursework HTML5 training application
This project is a learning application designed to learn HTML5. Users can take lessons, complete tests and track their progress. The application also provides the ability to log in via Google, and users can view their profile, including an avatar, overall progress and achievements.
<h1>Main Features</h1>
<ul>
  <li><b>Presentation of the main application form</b>: On the main form, the user can see a list of available lessons, information about the progress of lessons and buttons to go to the selected lesson.</li>
</ul>

![Group 9](https://github.com/LilScottyPippen/Coursework-WinForms-.Net/assets/126200705/e08bc726-3c91-401f-8c50-669c6f5d1a4e)

<ul>
  <li><b>Lesson form:</b> Each lesson is presented in a separate form, which contains material for study. At the end of each lesson, the user can take a test to test their knowledge.</li>
</ul>


![Group 9 (2)](https://github.com/LilScottyPippen/Coursework-WinForms-.Net/assets/126200705/573b0935-4bcd-4fef-a763-aefd0be2cbdf)


<ul>
  <li><b>Test form:</b> The test form contains the task condition and a place to write the answer. The user must enter a response and submit it for verification.</li>
</ul>

![Group 12](https://github.com/LilScottyPippen/Coursework-WinForms-.Net/assets/126200705/5c25293c-e74d-4ac5-9b1b-b2865ef3ea96)


<ul>
  <li><b>User Profile:</b> The user profile displays his avatar, overall progress in learning HTML5 and achievements. The user can track their progress and be motivated to achieve new goals.</li>
</ul>


![Group 13](https://github.com/LilScottyPippen/Coursework-WinForms-.Net/assets/126200705/dccbaf49-9945-4f05-b699-0e9ff6b1bb67)


<ul>
  <li><b>Authorization Form:</b> Users can log in to the app through their Google account. This ensures the convenience and safety of use.</li>
</ul>


![Group 11](https://github.com/LilScottyPippen/Coursework-WinForms-.Net/assets/126200705/ecea190c-0f81-417c-95d3-0900dd41cf5e)

<h1>Installation</h1>

1. Download the project from the repository: 
  ```
  git clone https://github.com/LilScottyPippen/Coursework-WinForms-.Net
  ```
2. In the root folder of the project, create `.env` and insert the contents inside it:
  ```
  DB_PASS=YOUR_DB_PASSWORD
  EMAIL_SENDER=YOUR_EMAIL_SENDER
  PASSWORD_SENDER=YOUR_PASSWORD_SENDER
  CLIENT_ID=YOUR_CLENT_ID
  CLIENT_SECRET=YOUR_CLIENT_SECRET
  EMAIL=USER_EMAIL(NULL)
  PASSWORD=USER_PASSWORD(NULL)
  GOOGLE_AUTH=true/false  
  TOKEN=YOUR_TOKEN
  ```
 3. Сreate DB:
  ```
  create table lesson
  (
      lesson_id serial primary key,
      title     text   not null,
      content   text   not null
  );

  create table users
  (
      user_id    serial       primary key,
      email      varchar(255) not null,
      password   varchar(255) not null,
      googleauth boolean      default false
  );

  create table google_credentials
  (
      id           serial    primary key,
      user_id      integer   not null references users,
      email        text      not null,
      access_token text not null
  );

  create table tests
  (
      id          serial   primary key,
      lesson_id   integer  references lesson,
      description text,
      answer      text,
      code        text
  );

  create table lesson_progress
  (
      user_id         integer not null  references users,
      lesson_id       integer not null  references lesson,
      progress        integer,
      passed_the_test boolean,
      primary key (user_id, lesson_id)
  );

  alter table lesson_progress
      owner to postgres;
  ```
 4. Run a query to the database to create a lesson.
  ```
  INSERT INTO lesson (lesson_id, title, content) 
  VALUES ('lesson_id_value', 'title_value', 'content_value');
  ```
  Content format:
  ```
  <h3>Here is a list of HTML, CSS and JavaScript editors you can choose from:</h3>
  <li>VS Code (free, recommended): </li> <a>https://code.visualstudio.com/</a> 
  <h2>Your first HTML Page</h2>
  <h3>Let's start by creating a simple HTML page. An HTML page has the following basic layout:</h3>
  <code><!DOCTYPE html><br><html><br>   <head><br>      <!-- head definitions go here --><br>   </head><br>   <body><br>      <!-- the content goes here --><br>   </body><br></html></code>
  <test></test>
  ```
  
<h2>Additional instructions</h2>
<ul>
  <li>PostgreSQL: https://www.postgresql.org/docs/current/app-psql.html</li>
  <li>Google Auth: https://afterlogic.com/mailbee-net/docs/OAuth2GoogleRegularAccountsInstalledApps.html</li>
  <li>Email sender: https://support.google.com/accounts/answer/185833?hl=en</li>
</ul>
