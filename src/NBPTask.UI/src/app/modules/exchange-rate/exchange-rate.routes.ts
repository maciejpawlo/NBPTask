import { Routes } from "@angular/router";
import { ExchangeRateListComponent } from "./pages/exchange-rate-list/exchange-rate-list.component";
import { authGuard } from "../core/guards/auth.guard";

export const exchangeRateRoutes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ExchangeRateListComponent,
    canActivate: [authGuard]
  }
];
