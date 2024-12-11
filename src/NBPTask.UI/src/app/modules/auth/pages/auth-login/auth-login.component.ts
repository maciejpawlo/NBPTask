import { Component, inject, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ButtonComponent } from "../../../core/ui/button/button.component";
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
@Component({
  selector: 'app-auth-login',
  standalone: true,
  imports: [MatCardModule, ButtonComponent, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './auth-login.component.html',
  styleUrl: './auth-login.component.css'
})
export class AuthLoginComponent {
  private authService: AuthenticationService = inject(AuthenticationService);
  private router: Router = inject(Router);
  private formBuilder: FormBuilder = inject(FormBuilder);

  loginForm: FormGroup = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  handleOnSubmit(): void {
    if(this.loginForm.valid) {
      this.authService.signIn(this.loginForm.value.username, this.loginForm.value.password)
        .subscribe({
          next: () => this.router.navigate(['']),
          error: () => console.log('TODO: show error message')
        });
    }
  }
}
