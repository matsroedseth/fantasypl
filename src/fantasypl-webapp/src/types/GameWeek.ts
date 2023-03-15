export interface GameWeek {
  id: number;
  name: string;
  deadline: Date;
  finished: boolean;
}

export interface GameWeekData {
  previous: GameWeek;
  current: GameWeek;
  next: GameWeek;
}
