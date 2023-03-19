import { Chip } from "./Enums";
import Manager from "./Manager";
import Player from "./Player";
import Team from "./Team";

export default interface Standing {
  managerInfo: Manager;
  activeChip: Chip | null;
  teamInfo: Team;
  players: Player[];
  gameWeekPoints: number;
  livePoints: number | null;
  currentRank: number;
  lastRank: number;
}
