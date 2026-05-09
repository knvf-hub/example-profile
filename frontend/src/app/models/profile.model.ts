import { Occupation } from './occupation.model';

export interface CreateProfileRequest {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  birthDate: string;
  occupationIds: number[];
  gender: string;
  profileBase64: string;
}

export interface CreateProfileResponse {
  id: number;
  message: string;
}

export interface GetProfileResponse {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  birthDate: string;
  occupations: Occupation[];
  gender: string;
  profileBase64: string;
}