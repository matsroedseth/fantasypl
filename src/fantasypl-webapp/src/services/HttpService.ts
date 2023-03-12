import axios, { AxiosInstance, AxiosResponse, AxiosError } from "axios";

enum StatusCode {
  Unauthorized = 401,
  Forbidden = 403,
  TooManyRequests = 429,
  InternalServerError = 500,
}

interface HttpResponse<T> extends AxiosResponse {
  data: T;
}

class HttpService {
  private axiosInstance: AxiosInstance;

  constructor(baseURL: string, headers?: Record<string, string>) {
    this.axiosInstance = axios.create({
      baseURL,
      headers,
    });
  }

  async get<T>(url: string): Promise<T> {
    try {
      const response: HttpResponse<T> = await this.axiosInstance.get(url);
      return response.data;
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status) {
        const status = axiosError.response.status;
        switch (status) {
          case StatusCode.Unauthorized:
            throw new Error("Unauthorized");
          case StatusCode.Forbidden:
            throw new Error("Forbidden");
          case StatusCode.TooManyRequests:
            throw new Error("Too Many Requests");
          case StatusCode.InternalServerError:
            throw new Error("Internal Server Error");
          default:
            throw new Error(`HTTP error: ${status}`);
        }
      } else {
        throw new Error("Unknown HTTP error");
      }
    }
  }
}

export default HttpService;
