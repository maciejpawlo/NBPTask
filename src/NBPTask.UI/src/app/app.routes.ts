import { Routes } from '@angular/router';
import { ExchangeRateListComponent } from './modules/exchange-rate/pages/exchange-rate-list/exchange-rate-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'exchange-rate',
    pathMatch: 'full',
  },
  {
    path: 'exchange-rate',
    loadChildren: () => import('./modules/exchange-rate/exchange-rate.routes').then(x => x.exchangeRateRoutes)
  },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.routes').then(x => x.authRoutes)
  }
];
