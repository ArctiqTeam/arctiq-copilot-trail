import { Request, Response } from 'express';
import { ChessPosition } from '../models/ChessPosition';

export class ChessController {
  static getPosition(req: Request, res: Response) {
    const fen = req.query.fen as string;
    const position = new ChessPosition(fen);
    res.json(position);
  }
}