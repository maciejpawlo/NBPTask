import { Routes } from "@angular/router";
import { AuthLoginComponent } from "./pages/auth-login/auth-login.component";
import { unauthGuard } from "../core/guards/unauth.guard";

export const authRoutes: Routes = [
  {
    path: 'login',
    component: AuthLoginComponent,
    canActivate: [unauthGuard]
  }
];
