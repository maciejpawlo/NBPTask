import { HttpErrorResponse, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, tap, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const snackbar = inject(MatSnackBar);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const problemDetails = error.error as ProblemDetails;
      //NOTE: here in production scenario we could get the user-friendly
      //translations for internal error codes
      snackbar.open(problemDetails.errorCode!, 'Zamknij', { duration: 5000, verticalPosition: 'top', horizontalPosition: 'right'});
      return throwError(() => error);
    })
  );
};

interface ProblemDetails {
     type?: string;
     title?: string;
     status?: number;
     detail?: string;
     instance?: string;
     errorCode?: string;
     validationErrors?: string[];
}
