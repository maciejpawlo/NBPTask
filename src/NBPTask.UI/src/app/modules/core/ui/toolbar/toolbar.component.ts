import { Component, inject, input } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthenticationService } from '../../services/authentication.service';
import { ButtonComponent } from "../button/button.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-toolbar',
  standalone: true,
  imports: [MatToolbarModule, ButtonComponent],
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.css'
})
export class ToolbarComponent {
  private authService = inject(AuthenticationService);
  private router = inject(Router);
  title = input.required<string>();

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  handleLogoutClicked(): void {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
