import Manager from "./Manager";

export default interface Standing {
  manager: Manager;
  gameWeekPoints: number;
  currentRank: number;
  lastRank: number;
}
