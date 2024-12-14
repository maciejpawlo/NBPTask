import { Component, inject, OnInit } from '@angular/core';
import { ExchangeRateApiService } from '../../../core/api/exchange-rate-api.service';
import { ExchangeRateDto } from '../../../core/api/responses/exchange-rate-dto';
import { GetExchangeRatesQuery } from '../../../core/api/requests/get-exchange-rates-query';
import { TableComponent } from "../../../core/ui/table/table.component";
import { Column } from '../../../core/ui/table/models/column';

@Component({
  selector: 'app-exchange-rate-list',
  standalone: true,
  imports: [TableComponent],
  templateUrl: './exchange-rate-list.component.html',
  styleUrl: './exchange-rate-list.component.css'
})
export class ExchangeRateListComponent implements OnInit {

  exchangeRates: ExchangeRateDto[] = [];
  columns: Column<ExchangeRateDto>[] = [
    {key: 'currency', displayName: 'Nazwa'},
    {key: 'mid', displayName: 'Kurs'},
    {key: 'code', displayName: 'Kod'}
  ];
  private exchangeRateApi = inject(ExchangeRateApiService);

  ngOnInit(): void {
    const request: GetExchangeRatesQuery = {
      tableType: 'A',
      topCount: 5
    };

    this.exchangeRateApi.getExchangeRates(request)
      .subscribe({
        next: (data) => {
          this.exchangeRates = data;
        }
      });
  }

}
