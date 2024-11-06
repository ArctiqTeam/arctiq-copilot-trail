import { Request, Response } from 'express';
import { ChessPosition } from '../models/ChessPosition';
import { v4 as uuidv4 } from 'uuid';

export class ChessController {
  private static games: Map<string, string> = new Map<string, string>();

  private static generateGameId(): string {
    return uuidv4().slice(0, 8);
  }

  public static addGame(req: Request, res: Response): void {
    const { fen } = req.body;
    if (!fen) {
      res.status(400).send('FEN string is required');
      return;
    }
    const gameId = ChessController.generateGameId();
    ChessController.games.set(gameId, fen);
    res.status(201).send({ gameId });
  }

  public static getGameById(req: Request, res: Response): void {
    const gameId = req.params.id;
    const fen = ChessController.games.get(gameId);
    if (!fen) {
      res.status(404).send('Game not found');
      return;
    }
    res.status(200).send({ fen });
  }

  public static getPosition(req: Request, res: Response): void {
    const fen = req.query.fen as string;
    const position = new ChessPosition(fen);
    res.json(position);
  }
}

export default ChessController;