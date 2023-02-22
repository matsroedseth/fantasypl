import http from "./HttpService";

class FantasyApi {
  // getAll(): Promise<any> {
  //   return http.get("/tutorials");
  // }

  getManager(id: number): Promise<any> {
    return http.get(`/managers/${id}`);
  }

  getLeague(id: number): Promise<any> {
    return http.get(`/leagues/${id}/standings`);
  }

  // findByTitle(title: string): Promise<any> {
  //   return http.get(`/tutorials?title=${title}`);
  // }
}

export default new FantasyApi();
