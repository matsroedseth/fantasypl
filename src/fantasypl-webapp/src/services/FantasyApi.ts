import LiveData from "../types/LiveData";
import http from "./HttpService";

class FantasyApi {
  // getAll(): Promise<any> {
  //   return http.get("/tutorials");
  // }

  getManager(id: number): Promise<any> {
    return http.get(`/managers/${id}`);
  }

  getLeagueInfoWithStandings(id: number): Promise<any> {
    return http.get(`/leagues/${id}/standings`);
  }

  getNextGameWeek(): Promise<any> {
    return http.get(`/gameweeks/next`);
  }

  getLivePoints(leagueId: number): Promise<any> {
    return http.get(`/leagues/${leagueId}/livedata`);
  }
}

export default new FantasyApi();
