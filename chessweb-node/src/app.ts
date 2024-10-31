import express from 'express';
import bodyParser from 'body-parser';
import { ChessController } from './controllers/ChessController';

const app = express();
const port = 3000;

app.use(bodyParser.json());
app.use(express.static('src/views'));

app.get('/position', ChessController.getPosition);

app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
});