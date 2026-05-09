import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Occupation } from '../models/occupation.model';

@Injectable({
  providedIn: 'root',
})
export class OccupationService {
    private http = inject(HttpClient);

    getOccupations(): Observable<Occupation[]> {
        return this.http.get<Occupation[]>('http://localhost:5002/api/Occupations');
    }
}
