import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TicketApi } from '../ticket-api';

@Component({
  selector: 'app-ticket-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './ticket-create.html',
  styleUrl: './ticket-create.css'
})
export class TicketCreate {
  private fb = inject(FormBuilder);
  private api = inject(TicketApi);
  private router = inject(Router);

  errorMessage = '';
  isLoading = false;

  form = this.fb.nonNullable.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    description: ['', [Validators.required]]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.api.createTicket(this.form.getRawValue()).subscribe({
      next: () => {
        this.router.navigate(['/tickets']);
      },
      error: () => {
        this.errorMessage = 'Не вдалося створити тікет. Спробуйте ще раз';
        this.isLoading = false;
      }
    });
  }
}
