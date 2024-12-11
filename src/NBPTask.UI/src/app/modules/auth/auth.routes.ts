import { Routes } from "@angular/router";
import { AuthLoginComponent } from "./pages/auth-login/auth-login.component";

export const authRoutes: Routes = [
  {
    path: 'login',
    component: AuthLoginComponent
  }
];
