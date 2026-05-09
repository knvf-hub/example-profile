
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateProfileRequest, CreateProfileResponse,  } from '../models/profile.model';
import { GetProfileResponse } from '../models/profile.model';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
    private http = inject(HttpClient);

    createProfile(payload: CreateProfileRequest) {
        return this.http.post<CreateProfileResponse>('http://localhost:5002/api/profiles',payload);
    }

    getProfiles() {
        return this.http.get<GetProfileResponse[]>('http://localhost:5002/api/profiles');
    }

    getProfile(id: number) {
        return this.http.get<GetProfileResponse>(`http://localhost:5002/api/profiles/${id}`);
    }
}
