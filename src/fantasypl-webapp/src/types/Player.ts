import { Position } from "./Enums";

export default interface Player {
  id: number;
  firstName: string;
  lastName: string;
  name: string;
  price: number;
  teamId: number;
  position: number;
  multiplier: number;
  isCaptain: number;
  isViceCaptain: string;
  elementType: Position;
}
