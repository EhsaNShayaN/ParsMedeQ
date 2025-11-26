import {BaseResult} from "./BaseResult";

export interface SendOtpRequest {
  mobile: string;
}

export interface SendOtpResponse {
  otp?: string;
  flag: boolean;
}

export interface CheckSigninRequest {
  mobile: string;
}

export interface CheckSigninResponse {
  result: string;
}

export interface MobileRequest {
  mobile: string;
  otp: string;
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
  email: string;
  mobile: string;
  passwordMustBeSet: boolean;
  isEmailConfirmed: boolean;
  isMobileConfirmed: boolean;
}
