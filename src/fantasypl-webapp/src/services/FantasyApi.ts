import GameWeek from "../types/GameWeek";
import LeagueWithStandings from "../types/LeagueWithStandings";
import LiveData from "../types/LiveData";
import HttpService from "./HttpService";

class FantasyApi {
  private http: HttpService;
  private baseURL = "https://localhost:5001/api";

  constructor() {
    this.http = new HttpService(this.baseURL);
  }

  async getLeagueInfoWithStandings(id: number): Promise<LeagueWithStandings> {
    return this.http.get(`/leagues/${id}/standings`);
  }

  async getNextGameWeek(): Promise<GameWeek> {
    return this.http.get<GameWeek>(`/gameweeks/next`);
  }

  async getLivePoints(leagueId: number): Promise<LiveData[]> {
    return this.http.get<LiveData[]>(`/leagues/${leagueId}/livedata`);
  }
}

export default new FantasyApi();
