export class ChessPosition {
    fen: string;
  
    constructor(fen: string) {
      this.fen = fen;
    }
  
    getFEN(): string {
      return this.fen;
    }
    // Add methods to parse and manipulate FEN notation
  }