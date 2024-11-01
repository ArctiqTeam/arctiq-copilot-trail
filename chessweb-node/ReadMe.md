# ChessWeb-Node Project

This project offers a path through the course material using NodeJS as the implementation language. The Application is a simple Chess viewer application that is imcomplete and has many opportunities for extension and improvement using Copilot.

## Setup

You will need to install npm prior to use. Once installed you can begin work on this project with:

```bash
npm install express
npm install --save-dev typescript @types/node @types/express
```

Then, you can run the web service with:

```bash
npm run build
npm start
```

The web app should be running on `http://localhost:3000` for you to try out.

## Capabilities

This application at present only provides a way to view a Chess board from its initial position and move the pieces around. There are some limited piece movement rules like:

- Pieces cannot take other pieces of their own colour
- Pawns obey their typical movement rules (except en-passant)
