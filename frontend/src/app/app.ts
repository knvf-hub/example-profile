import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, HostListener, inject, OnInit, signal, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

import { Occupation } from './models/occupation.model';
import { GetProfileResponse, CreateProfileRequest, CreateProfileResponse } from './models/profile.model';
import { OccupationService } from './services/occupation.service';
import { ProfileService } from './services/profile.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly profileService = inject(ProfileService);
  private readonly occupationService = inject(OccupationService);
  private readonly toastr = inject(ToastrService);
  private readonly cdr = inject(ChangeDetectorRef);
  protected readonly title = signal('Profile Management');
  @ViewChild('searchInput') searchInput!: ElementRef;
  @ViewChild('comboWrap') comboWrap!: ElementRef;
  occupations: Occupation[] = [];
  profiles: GetProfileResponse[] = [];
  selectedProfile: GetProfileResponse | null = null;
  message = '';
  loading = false;
  dropdownOpen = false;
  searchText = '';

  form = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, Validators.pattern(/^[0-9]{9,10}$/)]],
    birthDate: ['', Validators.required],
    occupationIds: [<number[]>[], Validators.required],
    gender: ['', Validators.required],
    profileBase64: ['', Validators.required]
  });

  ngOnInit(): void {
    this.loadOccupations();
    this.loadProfiles();
  }

  loadOccupations(): void {
    this.occupationService.getOccupations()
      .subscribe({
        next: (response: Occupation[]) => {
          console.log('occupations:', response);
          this.occupations = [...response];
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('load occupations error:', error);
          this.toastr.error('Unable to load occupations', 'Error');
        }
      });
  }

  loadProfiles(): void {
    this.profileService.getProfiles().subscribe({
      next: (response: GetProfileResponse[]) => {
        this.profiles = response;
        this.cdr.detectChanges();
      }
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toastr.error('Please complete all required fields', 'Validation Error');
      return;
    }

    this.loading = true;
    const payload = this.form.getRawValue() as CreateProfileRequest;

    this.profileService.createProfile(payload).subscribe({
      next: (response) => {
        this.toastr.success(`Save data success (ID: ${response.id})`, 'Success');
        this.form.reset({ occupationIds: [] });
        this.loadProfiles();
        this.loading = false;
      },

      error: () => {
        this.toastr.error('Unable to save profile', 'Error');
        this.loading = false;
      }
    });
  }

  clear(): void {
    this.form.reset({ occupationIds: [] });
    this.toastr.info('Form cleared', 'Information');
  }

  viewProfile(profile: GetProfileResponse): void {
    if (this.selectedProfile?.id === profile.id) {
      this.selectedProfile = null;
      return;
    }
    this.selectedProfile = profile;
  }

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files?.length) {
      return;
    }

    const file = input.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.form.patchValue({ profileBase64: reader.result as string });
    };
    reader.readAsDataURL(file);
  }

  get occupationsControl(): number[] {
    return this.form.controls.occupationIds.value ?? [];
  }

  toggleOccupation(id: number): void {
    const current = [...this.occupationsControl];
    const exists = current.includes(id);
    const value = exists ? current.filter(x => x !== id) : [...current, id];
    this.form.patchValue({ occupationIds: value });
  }

  getOccupationName(id: number): string {
    return this.occupations.find(occupation => occupation.id === id)?.name ?? '';
  }

  get filteredOccupations() {
    return this.occupations.filter(o =>
      o.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  focusSearch(): void {
    this.searchInput.nativeElement.focus();
    this.dropdownOpen = true;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    if (this.comboWrap && !this.comboWrap.nativeElement.contains(event.target)) {
      this.dropdownOpen = false;
      this.searchText = '';
    }
  }

  formatDate(dateStr: string): string {
    if (!dateStr) return '-';
    const [year, month, day] = dateStr.split('-');
    return `${day}/${month}/${year}`;
  }

  get today(): string {
    return new Date().toISOString().split('T')[0];
  }

  onEmailKeypress(event: KeyboardEvent): void {
    const allowed = /^[A-Za-z0-9.@]$/;
    if (!allowed.test(event.key)) {
      event.preventDefault();
    }
  }

  onPhoneKeypress(event: KeyboardEvent): void {
    const allowed = /^[0-9]$/;
    if (!allowed.test(event.key)) {
      event.preventDefault();
    }
  }
}