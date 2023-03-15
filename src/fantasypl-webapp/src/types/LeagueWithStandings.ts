import LeagueInfo from "./LeagueInfo";
import Standing from "./Standing";

export default interface LeagueWithStandings {
  league: LeagueInfo;
  standing: Standing[];
  captaincyPicks: CaptaincyPick[];
}

export interface CaptaincyPick {
  player: Captain;
  pickedByPercentage: number;
}

export interface Captain {
  id: number;
  firstName: string;
  lastName: string;
}
