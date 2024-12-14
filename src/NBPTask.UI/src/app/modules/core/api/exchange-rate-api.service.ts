import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExchangeRateDto } from './responses/exchange-rate-dto';
import { GetExchangeRatesQuery } from './requests/get-exchange-rates-query';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateApiService {

  private http: HttpClient = inject(HttpClient);
  private apiUrl: string = "https://localhost:7160/nbp"

  getExchangeRates(request: GetExchangeRatesQuery): Observable<ExchangeRateDto[]> {
    const params: HttpParams = new HttpParams()
      .set('tableType', request.tableType)
      .set('topCount', request.topCount);
    return this.http.get<ExchangeRateDto[]>(`${this.apiUrl}/exchange-rates`, { params: params });
  }
}