import {BaseResult} from "./BaseResult";

export interface SendOtpRequest {
  mobile: string;
}

export interface SendOtpResponse {
  otp?: string;
}

export interface CheckSigninRequest {
  mobile: string;
}

export interface CheckSigninResponse  {
  result: string;
}

export interface MobileRequest {
  mobile: string;
  otp: string;
}

export interface PasswordRequest {
  mobile: string;
  password: string;
}

export interface MobileResponse {
  token: string;
  fullname: string;
  mobile: string;
}

export interface ProfileResponse extends BaseResult<Profile> {
}

export interface Profile {
  id: number;
  fullName: string;
  firstName: string;
  lastName: string;
  email: string;
  mobile: string;
  nationalCode: string;
  passwordMustBeSet: boolean;
  isEmailConfirmed: boolean;
  isMobileConfirmed: boolean;
}
